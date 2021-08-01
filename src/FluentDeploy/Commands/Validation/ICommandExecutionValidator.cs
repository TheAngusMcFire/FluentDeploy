using FluentDeploy.ExecutionEngine;

namespace FluentDeploy.Commands.Validation
{
    public interface ICommandExecutionValidator
    {
        CommandExecutionValidationResult Validate(CommandExecutionResult result);
    }
}