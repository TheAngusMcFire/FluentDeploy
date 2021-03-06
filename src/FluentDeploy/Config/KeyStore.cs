using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Renci.SshNet;

namespace FluentDeploy.Config
{
    public class KeyStore
    {
        public List<PrivateKeyFile> PrivateKeyFiles;
        public static KeyStore Default { get; private set; }

        public static void InitKeyStore(string keyPath = null)
        {
            var path = keyPath ?? $"{Environment.GetFolderPath(Environment.SpecialFolder.UserProfile)}/.ssh";
            var files = Directory.GetFiles(path);
            var keyFiles = new List<PrivateKeyFile>();
            
            foreach (var file in files)
            {
                try
                {
                    keyFiles.Add(new PrivateKeyFile(file));
                }
                catch (Exception _)
                {
                    /* no valid key file todo log */
                }
            }
            
            Default = new KeyStore() { PrivateKeyFiles = keyFiles.ToList() };
        }

        public static void AddAdditionsPrivateKey(string key)
        {
            Default.PrivateKeyFiles.Add(new PrivateKeyFile(key));
        }
    }
}