using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using FluentDeploy.Commands;
using FluentDeploy.Commands.ExecutionControlCommands;
using FluentDeploy.Execution;
using FluentDeploy.ExecutionEngine.Interfaces;
using FluentDeploy.HostLogic;
using Serilog;

namespace FluentDeploy.ExecutionEngine
{
    public class ExecutionEngine
    {
        private readonly ILogger _logger;
        private readonly IHostCommandExecutor _commandExecutor;
        private bool _currentRootPrivilegeModifier = false;
        private bool _savedRootPrivilegeModifier = false;
        private readonly Host _host;

        public ExecutionEngine(Host host, IHostCommandExecutor commandExecutor)
        {
            _host = host;
            _commandExecutor = commandExecutor;
            _logger = Log.ForContext<ExecutionEngine>();
        }

        public void ExecuteHost() 
            => ExecuteCommands(_host.Context.Commands);
        

        private void ExecuteCommands(IList<BaseCommand> commands)
        {
            foreach (var command in commands)
            {
                DispatchCommand(command);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="command"></param>
        /// <returns>whether execution should be continued</returns>
        /// <exception cref="NotImplementedException"></exception>
        private bool DispatchCommand(BaseCommand command)
        {
            switch (command)
            {
                case ConsoleCommand cmd:
                    var result = _commandExecutor.ExecuteConsoleCommand(cmd, _currentRootPrivilegeModifier);

                    _logger.Debug($"Stdout:  {result.StdoutText}");
                    
                    if (!result.Success)
                    {
                        // todo some error handling here
                    }
                    
                    

                    return true;
                
                //* **** basically a wrapper for multiple commands **** *\\
                case ExecutionUnit cmd:
                    _logger.Information($"Execute commands for: {cmd.Name}");
                    ExecuteCommands(cmd.Commands);
                    return true;
                
                case ExecutionModifier cmd:
                    DispatchExecutionModifier(cmd);
                    return true;
                
                //* **** for commands which change their behavior based on what happens during execution **** *\\
                case IBaseActiveCommandBuilder cmd:
                    ExecuteCommands(cmd.GenerateCommands(_host.Context));
                    return true;
                
                default: 
                    throw new NotImplementedException(); 
            }
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
                    _savedRootPrivilegeModifier = _currentRootPrivilegeModifier;
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