using System;
using System.Buffers.Text;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Numerics;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace FluentDeploy.Config
{
    public class EncryptedConfigFile
    {
        public string Salt { get; set; }
        /* todo use on nonce per line and not a global one */
        public string Nonce { get; set; }
        public int Iterations { get; set; } 
        public string Tag { get; set; }
        public string[] Lines { get;set; }
    }

    public class EncryptedConfigFileHandler
    {
        private byte[] _nonce;
        private byte[] _key;
        private byte[] _refTag;
        private EncryptedConfigFile _file;
    
        public void Init(string path, string passPhrase)
        {
            _file = new EncryptedConfigFile();
            using var rng = new RNGCryptoServiceProvider();
            
            var salt = new byte[KeySize];
            _nonce = new byte[12];
            var iterationBytes = new byte[2];
            rng.GetNonZeroBytes(iterationBytes);
            rng.GetBytes(salt);
            rng.GetBytes(_nonce);

            var iterations = (((int)iterationBytes[0]) << 8 | iterationBytes[1]) + 1000; 
            var kdf = new Rfc2898DeriveBytes(passPhrase, salt, iterations);
            var lines = new [] {"---", "# Add config values here" };
            _file.Salt = Convert.ToBase64String(salt);
            _file.Iterations = iterations;
            _file.Nonce = Convert.ToBase64String(_nonce);
            _key = kdf.GetBytes(KeySize);
            _file.Lines = EncryptLines(rng, lines, _key , _nonce, out _refTag);
            _file.Tag = Convert.ToBase64String(_refTag); 
            SaveFile(path);
        }

        private void SaveFile(string path)
        {
            var fileStr = JsonSerializer.Serialize(_file, new JsonSerializerOptions()
            {
                WriteIndented = true
            });
            File.WriteAllText(path, fileStr);
        }

        public void Load(string path, string passphrase)
        {
            _file = JsonSerializer.Deserialize<EncryptedConfigFile>(File.ReadAllText(path));
            _nonce = Convert.FromBase64String(_file.Nonce);
            var salt = Convert.FromBase64String(_file.Salt);
            var iterations = _file.Iterations;
            _refTag = Convert.FromBase64String(_file.Tag);
            var kdf = new Rfc2898DeriveBytes(passphrase, salt, iterations);
            _key = kdf.GetBytes(KeySize);
        }

        private int KeySize => 32;
        private int TagSize => 16;
        private int PaddingSize => 1024;
        private int CalcPadding(int size) => ((size / KeySize) + (size % KeySize == 0 ? 0 : 1) + (size == 0 ? 1 : 0)) * KeySize;

        private byte[] GetPaddedArray(byte[] arr, byte[] padding)
        {
            var sz = CalcPadding(arr.Length);
            var resized = new byte[sz];
            Array.Copy(padding, 0, resized, 0, sz);
            Array.Copy(arr, 0, resized, 0, arr.Length);
            return resized;
        }
        
        private byte[] GetZeroPaddedArray(byte[] arr)
        {
            var sz = CalcPadding(arr.Length);
            var resized = new byte[sz];
            Array.Copy(arr, 0, resized, 0, arr.Length);
            return resized;
        }

        private string[] EncryptLines(RNGCryptoServiceProvider rng, string[] lines, byte[] key, byte[] nonce, out byte[] tag)
        {
            var encLines = new List<string>();
            var aes = new AesGcm(key);
            var padding = new byte[PaddingSize];
            tag = new byte[TagSize];
            var stream = new MemoryStream();
            
            foreach (var line in lines)
            {
                rng.GetNonZeroBytes(padding);
                var size = CalcPadding(line.Length);
                var cipherText = new byte[size];
                var payload = Encoding.Default.GetBytes(line);
                stream.Write(payload);
                var plaintext = GetPaddedArray(payload, padding);
              
                if (line.Length < size)
                {
                    plaintext[line.Length] = 0;
                }
                
                aes.Encrypt(nonce, plaintext, cipherText, tag);
                encLines.Add($"{Convert.ToBase64String(cipherText)};{Convert.ToBase64String(tag)}");
            }
            stream.Write(Encoding.Default.GetBytes(_file.Nonce));
            stream.Write(Encoding.Default.GetBytes(_file.Salt));
            stream.Write(Encoding.Default.GetBytes(_file.Iterations.ToString()));
            var pt = GetZeroPaddedArray(stream.ToArray());
            var ct = new byte[pt.Length];
            aes.Encrypt(nonce, pt, ct, tag);
            return encLines.ToArray();
        }
        
        private string[] DecryptLines(string[] lines, byte[] key, byte[] nonce, byte[] refTagTotal)
        {
            var decLines = new List<string>();
            var aes = new AesGcm(key);
            var stream = new MemoryStream();
            
            foreach (var line in lines)
            {
                var comps = line.Split(new[] {";"}, StringSplitOptions.RemoveEmptyEntries);
                var cipherText = Convert.FromBase64String(comps[0]);
                var refTag = Convert.FromBase64String(comps[1]);
                var plainText = new byte[cipherText.Length];
                aes.Decrypt(nonce, cipherText, refTag, plainText);
                var zeroIndex = Array.IndexOf(plainText, (byte)0);
                var target = zeroIndex < 0 ? plainText : plainText.Take(zeroIndex).ToArray(); 
                stream.Write(target);
                decLines.Add(Encoding.Default.GetString(target));
            }
            
            stream.Write(Encoding.Default.GetBytes(_file.Nonce));
            stream.Write(Encoding.Default.GetBytes(_file.Salt));
            stream.Write(Encoding.Default.GetBytes(_file.Iterations.ToString()));
            var pt = GetZeroPaddedArray(stream.ToArray());
            var ct = new byte[pt.Length];
            var tag = new byte [TagSize];
            aes.Encrypt(nonce, pt, ct, tag);

            if (!tag.SequenceEqual(refTagTotal))
            {
                throw new InvalidOperationException("Error the Tags do not match");
            }

            return decLines.ToArray();
        }

        public string GetDecryptedFileContent()
        {
            var builder = new StringBuilder();
            var lines = DecryptLines(_file.Lines, _key, _nonce, _refTag);

            foreach (var line in lines)
            {
                builder.AppendLine(line);
            }
            
            return builder.ToString();
        }

        public void Edit(string path, string passphrase)
        {
            var editor = Environment.GetEnvironmentVariable("EDITOR") 
                         ?? throw new InvalidOperationException("No EDITOR variable set");
            
            Load(path, passphrase);
            
            var decLines = DecryptLines(_file.Lines, _key, _nonce, _refTag);
            var tmpFileName = $"/tmp/dec_fd_{DateTime.Now.Ticks}.yaml";
            
            File.WriteAllLines(tmpFileName, decLines);
            
            var process = new Process();
            process.StartInfo.FileName = editor;
            process.StartInfo.Arguments = tmpFileName;
            process.Start();
            
            process.WaitForExit();
            
            decLines = File.ReadAllLines(tmpFileName);
            
            var encLines = EncryptLines(new RNGCryptoServiceProvider(), decLines, _key, _nonce, out _refTag);
            _file.Lines = encLines;
            _file.Tag = Convert.ToBase64String(_refTag);
            SaveFile(path);
            File.Delete(tmpFileName);
        }
    }
}