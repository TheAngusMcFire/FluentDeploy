using System.Collections.Generic;
using FluentDeploy.Commands;
using FluentDeploy.Commands.ExecutionControlCommands;

namespace FluentDeploy.Execution
{
    public class HostContext : ICommandContext<HostContext>, ICommandContext
    {
        private List<BaseCommand> _commands = new();
        
        public HostContext AddCommand(BaseCommand command)
        {
            _commands.Add(command);
            return this;
        }

        public bool PackageManagerMirrorsUpdated { get; set; }
    }
}