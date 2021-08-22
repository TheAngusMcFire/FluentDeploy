using FluentDeploy.ExecutionEngine;
using FluentDeploy.ExecutionEngine.ExecutionResults;

namespace FluentDeploy.Commands.Validation
{
    public interface ICommandExecutionValidator
    {
        CommandExecutionValidationResult Validate(CommandExecutionResult result);
    }
}