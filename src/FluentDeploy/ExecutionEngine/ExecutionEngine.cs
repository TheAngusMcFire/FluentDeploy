using System;
using System.Linq;
using FluentDeploy.Commands;
using FluentDeploy.Commands.ExecutionControlCommands;
using FluentDeploy.Enums;
using FluentDeploy.Exceptions;
using FluentDeploy.ExecutionEngine.ExecutionResults;
using FluentDeploy.ExecutionEngine.Interfaces;
using FluentDeploy.HostLogic;
using Serilog;

namespace FluentDeploy.ExecutionEngine
{
    public class ExecutionEngine : ICommandExecutor
    {
        private readonly ILogger _logger;
        private readonly IHostCommandExecutor _commandExecutor;
        private bool _currentRootPrivilegeModifier;
        private bool _savedRootPrivilegeModifier;
        private readonly Host _host;

        public ExecutionEngine(Host host, IHostCommandExecutor commandExecutor)
        {
            _host = host;
            _commandExecutor = commandExecutor;
            _logger = Log.ForContext<ExecutionEngine>();
        }

        public CommandExecutionResult ExecuteCommand(BaseCommand cmd)
        {
            return DispatchCommand(cmd);
        }
        
        private CommandExecutionResult DispatchCommand(BaseCommand command)
        {
            CommandExecutionResult result;

            switch (command)
            {
                case OutputTextSeparatorCommand cmd:
                    _logger.Information($"// ************************ {cmd.CommandName} ************************ \\\\");
                    _logger.Information($"Description: {cmd.UserDescription}");
                    _logger.Information($"// -------------------------{new string(cmd.CommandName.Select(_ => '-').ToArray())}------------------------- \\\\\n");
                    result = new CommandExecutionResult();
                    break;
                
                case ConsoleCommand cmd:
                    result = _commandExecutor.ExecuteConsoleCommand(cmd, _currentRootPrivilegeModifier);
                    break;

                case FileOperationCommand {FileOperationType: FileOperationType.CreateDirectory} cmd:
                    result = _commandExecutor.CreateDirectory(cmd, _currentRootPrivilegeModifier);
                    break;
                
                case FileOperationCommand {FileOperationType: FileOperationType.CreateFile} cmd:
                    result = _commandExecutor.CreateFile(cmd, _currentRootPrivilegeModifier);
                    break;
                
                case FileOperationCommand {FileOperationType: FileOperationType.Delete} cmd:
                    result = _commandExecutor.Delete(cmd, _currentRootPrivilegeModifier);
                    break;
                
                case FileOperationCommand {FileOperationType: FileOperationType.Exists} cmd:
                    result = _commandExecutor.Exists(cmd, _currentRootPrivilegeModifier);
                    break;
                
                case FileOperationCommand {FileOperationType: FileOperationType.SymbolicLink} cmd:
                    result = _commandExecutor.SymbolicLink(cmd, _currentRootPrivilegeModifier);
                    break;
                
                case FileOperationCommand {FileOperationType: FileOperationType.CopyFromLocal} cmd:
                    result = _commandExecutor.CopyLocalFile(cmd, _currentRootPrivilegeModifier);
                    break;
                
                case ExecutionModifier cmd:
                    DispatchExecutionModifier(cmd);
                    result = CommandExecutionResult.SuccessResult;
                    break;

                default: 
                    throw new NotImplementedException(); 
            }

            result.PrintResultData(_logger.Debug);
            var validationResult = command.Validator.Validate(result);
            result.ValidationResult = validationResult;

            if (validationResult.WasSuccessful)
            {
                return result;
            }
            
            result.PrintResultData(_logger.Error);
            throw new CommandValidationException(validationResult.ErrorMessage);
        }

        private void DispatchExecutionModifier(ExecutionModifier cmd)
        {
            switch (cmd.ModifierType)
            {
                case ExecutionModifierType.RunAsRoot:
                    _savedRootPrivilegeModifier = _currentRootPrivilegeModifier;
                    _currentRootPrivilegeModifier = true;
                    break;
                case ExecutionModifierType.RunAsUser:
                    _savedRootPrivilegeModifier = _currentRootPrivilegeModifier;
                    _currentRootPrivilegeModifier = false;
                    break;
                case ExecutionModifierType.ResetPrivilegeChange:
                    _currentRootPrivilegeModifier = _savedRootPrivilegeModifier;
                    break;
                case ExecutionModifierType.PackageManagerUpdated:
                    _host.Context.PackageManagerMirrorsUpdated = true;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

    }
}