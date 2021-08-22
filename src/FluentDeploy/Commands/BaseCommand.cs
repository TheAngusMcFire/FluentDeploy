using FluentDeploy.Commands.Validation;
using Serilog;

namespace FluentDeploy.Commands
{
    public abstract class BaseCommand
    {
        public virtual ICommandExecutionValidator Validator =>
            new ConstResultCommandExecutionValidator(CommandExecutionValidationResult.SuccessResult);
    }
}