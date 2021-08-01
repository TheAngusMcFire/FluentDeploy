using FluentDeploy.Commands;
using FluentDeploy.ExecutionEngine;
using FluentDeploy.ExecutionEngine.Interfaces;
using FluentDeploy.ExecutionUtils.Interfaces;

namespace FluentDeploy.ExecutionUtils
{
    public class HostContext : IExecutionContext
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