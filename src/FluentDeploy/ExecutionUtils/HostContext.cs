using System.Collections.Generic;
using FluentDeploy.Commands;
using FluentDeploy.Commands.ExecutionControlCommands;
using FluentDeploy.ExecutionEngine;
using FluentDeploy.ExecutionEngine.Interfaces;

namespace FluentDeploy.Execution
{
    public class HostContext : ICommandContext
    {
        private readonly ICommandExecutor _commandExecutor;

        public HostContext(ICommandExecutor commandExecutor)
        {
            _commandExecutor = commandExecutor;
        }

        public bool PackageManagerMirrorsUpdated { get; set; }
        public CommandExecutionResult ExecuteCommand(BaseCommand cmd)
        {
            return _commandExecutor.ExecuteCommand(cmd);
        }
    }
}