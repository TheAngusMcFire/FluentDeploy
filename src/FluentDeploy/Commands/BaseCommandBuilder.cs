using System.Collections.Generic;
using FluentDeploy.Execution;

namespace FluentDeploy.Commands
{
    public abstract class BaseCommandBuilder <T> where T : BaseCommandBuilder<T>
    {
        private bool _runAsRoot;
        public string Name { get; set; }
        public string Description { get; set; }

        public T RunAsRoot()
        {
            _runAsRoot = true;
            return (T) this;
        }

        protected abstract List<BaseCommand> BuildCommands(IHostInfo hostInfo);

        public void SaveTo(ICommandContext context)
        {
            if(_runAsRoot)
            {
                context.AddCommand(CommandStore.AsRootCommand());
            }
            
            var eu = new ExecutionUnit()
            {
                Commands = BuildCommands(context),
                Name = Name,
                Description = Description
            };
            
            context.AddCommand(eu);
        }
    }
}