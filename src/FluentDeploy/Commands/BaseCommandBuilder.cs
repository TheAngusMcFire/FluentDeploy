using System;
using FluentDeploy.Commands.ExecutionControlCommands;
using FluentDeploy.ExecutionUtils.Interfaces;

namespace FluentDeploy.Commands
{
    public abstract class BaseCommandBuilder <T> where T : BaseCommandBuilder <T>
    {
        public bool SuppressOutput { get; set; } = false;
        private bool _runAsRoot;
        protected string Name { get; set; } = typeof(T).Name;
        protected string UserDescription { get; set; } = $"Execution of {typeof(T).Name}";

        public T Description(string description)
        {
            UserDescription = description;
            return (T) this;
        }
        
        public T RunAsRoot()
        {
            _runAsRoot = true;
            return (T) this;
        }

        public T FluentExec(Action action)
        {
            action();
            return (T) this;
        }

        protected abstract void Execute(IExecutionContext context);

        public T ExecuteOn(IExecutionContext context)
        {
            PreExecution(context);
            Execute(context);
            PostExecution(context);
            return (T) this;
        }

        private void PreExecution(IExecutionContext context)
        {
            if (Name != null || UserDescription != null)
            {
                if(SuppressOutput)
                    return;
            
                context.ExecuteCommand(new OutputTextSeparatorCommand()
                {
                    CommandName = Name,
                    UserDescription = UserDescription
                });
            }

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
