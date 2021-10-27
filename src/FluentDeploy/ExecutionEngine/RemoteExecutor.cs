using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using FluentDeploy.Commands;
using FluentDeploy.ExecutionEngine.ExecutionResults;
using FluentDeploy.ExecutionEngine.Interfaces;
using Renci.SshNet;
using Serilog;

namespace FluentDeploy.ExecutionEngine
{
    public class RemoteExecutor : IHostCommandExecutor, IDisposable
    {
        private SshClient _sshClient;
        private SftpClient _sftpClient;
        private readonly ILogger _logger;
        private readonly ConnectionInfo _connectionInfo;

        private SshClient SshClient 
        {
            get
            {
                if (_sshClient == null || _sshClient.IsConnected == false)
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
                if (_sftpClient == null || _sftpClient.IsConnected == false)
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
            _connectionInfo = new ConnectionInfo(host, port, username, new AuthenticationMethod[]
            {
                new PrivateKeyAuthenticationMethod(username, privateKeys)
            });
        }

        public (string host, int port, IDisposable handle) EstablishPortForwarding(string localInterface, string remoteInterface, int port)
        {
            var sshClient = SshClient;
            var portForwarding = new ForwardedPortLocal(localInterface, 22345, remoteInterface, (uint)port);
            sshClient.AddForwardedPort(portForwarding);
            portForwarding.Start();
            return (portForwarding.BoundHost, (int)portForwarding.BoundPort, portForwarding);
        }

        public ConsoleCommandExecutionResult ExecuteConsoleCommand(ConsoleCommand cmd, bool asRoot)
        {
            var args = string.Join(" ", cmd.Arguments.Select(x => $"'{x}'").ToArray());
            var withRoot = asRoot ? "sudo " : string.Empty;
            var cmdLine = args.Length == 0 ? $"{withRoot}{cmd.ExecutableName}": $"{withRoot}{cmd.ExecutableName} {args}";
            cmdLine = cmd.WorkingDir == null ? cmdLine : $"cd '{cmd.WorkingDir}' && {cmdLine} && cd";
            _logger.Debug($"Execute Command: {cmdLine}");
            var sshCmd = SshClient.CreateCommand(cmdLine);
            sshCmd.CommandTimeout = TimeSpan.FromSeconds(cmd.Timeout);
            var stdOut = sshCmd.Execute();
            var stdErr = sshCmd.Error;
            var returnCode = sshCmd.ExitStatus;
            return new ConsoleCommandExecutionResult()
            {
                CommandLine = cmdLine,
                ReturnCode = returnCode,
                StdOutText = stdOut,
                StdErrText = stdErr
            };
        }

        private IEnumerable<string> GetPathsParts(string path)
        {
            var lst = new List<string>();
            var dirInfo = new DirectoryInfo(path);

            while (dirInfo is not null)
            {
                lst.Add(dirInfo.FullName);
                dirInfo = dirInfo.Parent;
            }

            lst.Reverse();

            return lst;
        }

        public FileOperationExecutionResult CreateDirectory(FileOperationCommand command, bool asRoot /* rfu */)
        {
            var sftpClient = SftpClient;
            var getParts = GetPathsParts(command.Path);

            foreach (var path in getParts)
            {
                if(!sftpClient.Exists(path))
                {
                    sftpClient.CreateDirectory(path);
                }
            }

            SetAttributes(command);
            return new FileOperationExecutionResult(){Exists = true};;
        }

        public FileOperationExecutionResult CreateFile(FileOperationCommand command, bool asRoot /* rfu */)
        {
            var sftpClient = SftpClient;
            using var stream = sftpClient.Create(command.Path);
            stream.Write(command.FileContent);
            stream.Flush();
            
            SetAttributes(command);
            
            return new FileOperationExecutionResult() {Exists = true};
        }

        public FileOperationExecutionResult Delete(FileOperationCommand command, bool asRoot /* rfu */)
        {
            var sftpClient = SftpClient;
            sftpClient.Delete(command.Path);
            return new FileOperationExecutionResult() {Exists = false};
        }
        
        public FileOperationExecutionResult Exists(FileOperationCommand command, bool asRoot /* rfu */)
        {
            var sftpClient = SftpClient;
            var exists = sftpClient.Exists(command.Path);
            return new FileOperationExecutionResult() {Exists = exists};
        }

        public FileOperationExecutionResult SymbolicLink(FileOperationCommand command, bool asRoot /* rfu */)
        {
            var sftpClient = SftpClient;
            sftpClient.SymbolicLink(command.Source, command.Destination);
            return new FileOperationExecutionResult() {Exists = true};
        }
        
        public FileOperationExecutionResult CopyLocalFile(FileOperationCommand command, bool asRoot /* rfu */)
        {
            var sftpClient = SftpClient;
            using var stream = sftpClient.Create(command.Path);
            var file = File.Open(command.Source, FileMode.Open);
            file.CopyTo(stream);
            stream.Flush();
            
            SetAttributes(command);
            
            return new FileOperationExecutionResult() {Exists = true};
        }

        public FileOperationExecutionResult SetFileAttributes(FileOperationCommand command, bool asRoot)
        {
            SetAttributes(command);
            return new FileOperationExecutionResult() {Exists = true};
        }
        
        private void SetAttributes(FileOperationCommand command)
        {
            var sftpClient = SftpClient;
            var attributes = sftpClient.GetAttributes(command.Path);
            attributes.UserId = command.UserId ?? attributes.UserId;
            attributes.GroupId = command.GroupId ?? attributes.GroupId;

            if (command.Permissions.HasValue)
            {
                attributes.SetPermissions(command.Permissions.Value);    
            }
            
            sftpClient.SetAttributes(command.Path, attributes);
        }

        public void Dispose()
        {
            _sshClient?.Dispose();
            _sftpClient?.Dispose();
        }
    }
}