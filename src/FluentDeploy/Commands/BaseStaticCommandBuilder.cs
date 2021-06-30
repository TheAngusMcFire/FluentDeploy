using System.Collections.Generic;
using FluentDeploy.Execution;

namespace FluentDeploy.Commands
{
    public abstract class BaseStaticCommandBuilder <T> where T : BaseStaticCommandBuilder<T>
    {
        private bool _runAsRoot;
        protected string Name { get; set; }
        protected string Description { get; set; }

        public T RunAsRoot()
        {
            _runAsRoot = true;
            return (T) this;
        }

        protected abstract List<BaseCommand> BuildCommands(IHostInfo hostInfo);

        public void SaveTo(ICommandContext context)
        {
            var cmds = new List<BaseCommand>();
            
            if(_runAsRoot)
            {
                cmds.Add(CommandStore.AsRootCommand());
            }
            
            cmds.AddRange(BuildCommands(context));
            
            if(_runAsRoot)
            {
                cmds.Add(CommandStore.ResetPrivilegeChange());
            }
            
            var eu = new ExecutionUnit()
            {
                Commands = cmds,
                Name = Name,
                Description = Description
            };
            
            context.AddCommand(eu);
        }
    }
}
