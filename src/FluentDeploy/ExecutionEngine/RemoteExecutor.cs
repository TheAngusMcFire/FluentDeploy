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
        private SshClient _client;
        private readonly ILogger _logger; 
        
        public RemoteExecutor(HostInformation information)
        {
            _logger = Log.ForContext<RemoteExecutor>();
            var hostComps = information.Host.Split(new[] {":"}, StringSplitOptions.RemoveEmptyEntries);
            
            // todo validate host info and report proper error \\
            var host = hostComps.First();
            var port = hostComps.Select((x, i) => (x, i))
                .Where(x => x.i == 1)
                .Select(x => Convert.ToInt32(x.x))
                .DefaultIfEmpty(22)
                .First();
            
            _logger.Debug($"Connect to host: {information.Host}");
            
            _client = new SshClient(host, port, information.User, KeyStore.Default.PrivateKeyFiles);
            _client.Connect();
        }
        
        public CommandExecutionResult ExecuteConsoleCommand(ConsoleCommand cmd, bool asRoot)
        {
            var args = string.Join(" ", cmd.Arguments.Select(x => $"'{x.Replace("\"", "\\\"")}'").ToArray());
            var withRoot = asRoot ? "sudo " : string.Empty;
            var cmdLine = $"{withRoot}{cmd.ExecutableName} {args}";
            _logger.Debug($"Execute Command: {cmdLine}");
            var sshCmd = _client.CreateCommand(cmdLine);
            var txt = sshCmd.Execute();
            var returnCode = sshCmd.ExitStatus;
            return new CommandExecutionResult() { Success = returnCode == 0, StdoutText = txt};
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
        
        private void DispatchCommand(BaseCommand command)
        {
            switch (command)
            {
                case ConsoleCommand cmd: 
                    ExecuteConsoleCommand(cmd);
                    return;
                    
                    default: 
                        throw new NotImplementedException(); 
            }
        }

        public void ExecuteCommands(List<BaseCommand> commands)
        {
            try
            {
                foreach (var cmd in commands)
                {
                    DispatchCommand(cmd);
                }
            }
            catch (Exception exc)
            {
                Console.WriteLine("Error while executing commands " + exc.ToString());
            }
            finally
            {
                _client.Disconnect();
                _client.Dispose();
            }
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