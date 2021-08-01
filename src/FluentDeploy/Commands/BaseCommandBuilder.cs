using System.Collections.Generic;
using FluentDeploy.ExecutionEngine.Interfaces;
using FluentDeploy.ExecutionUtils.Interfaces;

namespace FluentDeploy.Commands
{
    public abstract class BaseCommandBuilder <T> where T : BaseCommandBuilder <T> 
    {
        private bool _runAsRoot;
        protected string Name { get; set; }
        protected string Description { get; set; }

        public T RunAsRoot()
        {
            _runAsRoot = true;
            return (T) this;
        }

        protected abstract void Execute(IExecutionContext executor);

        public void ExecuteOn(IExecutionContext context)
        {
            PreExecution(context);
            Execute(context);
            PostExecution(context);
        }

        private void PreExecution(IExecutionContext context)
        {
            if(_runAsRoot)
            {
                context.ExecuteCommand(CommandStore.AsRootCommand());
            }
        }

        private void PostExecution(IExecutionContext context)
        {
            if(_runAsRoot)
            {
                context.ExecuteCommand(CommandStore.ResetPrivilegeChange());
            }
        }
    }
}
