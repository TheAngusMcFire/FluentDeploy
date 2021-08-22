using System;
using System.Collections.Generic;
using System.Linq;
using FluentDeploy.Commands;
using FluentDeploy.Config;
using FluentDeploy.ExecutionEngine.Interfaces;
using Renci.SshNet;
using Serilog;

namespace FluentDeploy.ExecutionEngine
{
    public class RemoteExecutor : IHostCommandExecutor
    {
        private SshClient _sshClient;
        private SftpClient _sftpClient;
        private readonly ILogger _logger;
        private readonly ConnectionInfo _connectionInfo;

        private SshClient SshClient 
        {
            get
            {
                if (_sshClient == null)
                {
                    _sshClient = new SshClient(_connectionInfo);
                    _sshClient.Connect();
                }

                return _sshClient;
            }
        }
        
        private SftpClient SftpClient
        {
            get
            {
                if (_sftpClient == null)
                {
                    _sftpClient = new SftpClient(_connectionInfo);
                    _sftpClient.Connect();
                }

                return _sftpClient;
            }
        }

        public RemoteExecutor(string host, int port, string username, PrivateKeyFile[] privateKeys)
        {
            _logger = Log.ForContext<RemoteExecutor>();
            //var hostComps = information.Host.Split(new[] {":"}, StringSplitOptions.RemoveEmptyEntries);
            
            // todo validate host info and report proper error \\
           
            
            //_logger.Debug($"Connect to host: {information.Host}");
            
            _connectionInfo = new ConnectionInfo(host, port, username, new AuthenticationMethod[]
            {
                new PrivateKeyAuthenticationMethod(username, privateKeys)
            });
        }
        
        public CommandExecutionResult ExecuteConsoleCommand(ConsoleCommand cmd, bool asRoot)
        {
            var args = string.Join(" ", cmd.Arguments.Select(x => $"'{x.Replace("\"", "\\\"")}'").ToArray());
            var withRoot = asRoot ? "sudo " : string.Empty;
            var cmdLine = $"{withRoot}{cmd.ExecutableName} {args}";
            _logger.Debug($"Execute Command: {cmdLine}");
            var sshCmd = SshClient.CreateCommand(cmdLine);
            var txt = sshCmd.Execute();
            var returnCode = sshCmd.ExitStatus;
            return new CommandExecutionResult() { Success = returnCode == 0, StdoutText = txt};
        }

        public CommandExecutionResult CreateDirectory(string path, bool asRoot)
        {
            var sftpClient = SftpClient;
            //sftpClient.CreateDirectory();
            return null;
        }

        private void ExecuteConsoleCommand(ConsoleCommand cmd)
        {
            //var args = string.Join(" ", cmd.Arguments.Select(x => $"'{x.Replace("\"", "\\\"")}'").ToArray());
            //var withRoot = cmd.WithRoot ? "sudo " : string.Empty;
            //var sshCmd = _client.CreateCommand($"{withRoot}{cmd.ExecutableName} {args}");
            //var txt = sshCmd.Execute();
            //var returnCode = sshCmd.ExitStatus;
            //cmd.Validator.Validate(returnCode, sshCmd.OutputStream);
        }


        public void Test()
        {
            
            //Renci.SshNet.

            //var stream = client.CreateShellStream("term1", 100, 100, 100, 100, 100);
            
            /*
            var inStream = new PipeStream();
            var outStream = new PipeStream();
            outStream.BlockLastReadBuffer = false;

            //var reader = new StreamReader(outStream);
            var writer = new StreamWriter(inStream);

            var shell = client.CreateShell(inStream, outStream, Stream.Null);
            shell.Start();
            
            
            writer.WriteLine("ls /bin\n");
            writer.Flush();
            Task.Delay(100).Wait();
            var buffer = new byte[500];
            //var vals = outStream.Read(buffer, 0, buffer.Length);
            outStream.BlockLastReadBuffer = false;
            var reader = new StreamReader(outStream, Encoding.UTF8, true, 1, false);
            //writer.Flush();
            while (true)
            {
                Console.WriteLine(reader.ReadLine());
            }
                */
            Console.ReadLine();

            //for (var i = 0; i < 100; i++)
            //{
            //    var cmd = client.CreateCommand("read");
            //    var txt = cmd.BeginExecute();
            //    txt.AsyncWaitHandle.WaitOne();
            //    var code = cmd.ExitStatus;
            //    Console.WriteLine(txt);    
            //}
        }
    }
}