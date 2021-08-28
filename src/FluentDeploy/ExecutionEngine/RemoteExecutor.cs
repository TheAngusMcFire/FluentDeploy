using System;
using System.IO;
using System.Linq;
using FluentDeploy.Commands;
using FluentDeploy.ExecutionEngine.ExecutionResults;
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

        public FileOperationExecutionResult CreateDirectory(FileOperationCommand command, bool asRoot /* rfu */)
        {
            var sftpClient = SftpClient;
            
            if(!sftpClient.Exists(command.Path))
            {
                sftpClient.CreateDirectory(command.Path);
            }
            
            var attributes = sftpClient.GetAttributes(command.Path);
            attributes.UserId = command.UserId ?? attributes.UserId;
            attributes.GroupId = command.GroupId ?? attributes.GroupId;

            if (command.Permissions.HasValue)
            {
                attributes.SetPermissions(command.Permissions.Value);    
            }
            
            sftpClient.SetAttributes(command.Path, attributes);
            return new FileOperationExecutionResult();
        }
        
        public FileOperationExecutionResult CreateFile(FileOperationCommand command, bool asRoot /* rfu */)
        {
            var sftpClient = SftpClient;
            using var stream = sftpClient.Create(command.Path);
            stream.Write(command.FileContent);
            stream.Flush();
            
            var attributes = sftpClient.GetAttributes(command.Path);
            attributes.UserId = command.UserId ?? attributes.UserId;
            attributes.GroupId = command.GroupId ?? attributes.GroupId;

            if (command.Permissions.HasValue)
            {
                attributes.SetPermissions(command.Permissions.Value);    
            }
            
            sftpClient.SetAttributes(command.Path, attributes);
            
            return new FileOperationExecutionResult();
        }  
    }
}