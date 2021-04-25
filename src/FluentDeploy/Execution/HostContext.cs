using System.Collections.Generic;
using FluentDeploy.Commands;
using FluentDeploy.Commands.ExecutionControlCommands;

namespace FluentDeploy.Execution
{
    public class HostContext : ICommandContext
    {
        private List<BaseCommand> _commands = new();
        
        public void AddCommand(BaseCommand command)
        {
            _commands.Add(command);
        }

        public bool PackageManagerMirrorsUpdated { get; set; }
    }
}