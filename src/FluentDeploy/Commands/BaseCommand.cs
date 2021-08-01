using FluentDeploy.Commands.Validation;
using Serilog;

namespace FluentDeploy.Commands
{
    public abstract class BaseCommand
    {
        public abstract ICommandExecutionValidator Validator { get; } 
    }
}