using FluentDeploy.Execution;

namespace FluentDeploy.Commands
{
    public abstract class BaseCommand
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public virtual ICommandExecutionValidator Validator { get; set; } 
    }
}