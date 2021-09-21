using FluentDeploy.Commands;
using FluentDeploy.ExecutionUtils.Interfaces;

namespace FluentDeploy.Components.Etc
{
    public class DirectExecutionBuilder : BaseCommandBuilder<DirectExecutionBuilder>
    {
        private readonly BaseCommand _command;

        public DirectExecutionBuilder(BaseCommand command)
        {
            _command = command;
        }

        protected override void Execute(IExecutionContext context)
        {
            context.ExecuteCommand(_command);
        }
    }
}