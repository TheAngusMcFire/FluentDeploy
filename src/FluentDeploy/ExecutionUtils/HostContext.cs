using System.Collections.Generic;
using FluentDeploy.Commands;
using FluentDeploy.Commands.ExecutionControlCommands;

namespace FluentDeploy.Execution
{
    public class HostContext : ICommandContext<HostContext>, ICommandContext
    {
        public List<BaseCommand> Commands { get; } = new();
        
        public HostContext AddCommand(BaseCommand command)
        {
            Commands.Add(command);
            return this;
        }

        public bool PackageManagerMirrorsUpdated { get; set; }
    }
}