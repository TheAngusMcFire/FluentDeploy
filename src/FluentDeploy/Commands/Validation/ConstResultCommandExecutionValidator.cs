using FluentDeploy.ExecutionEngine;

namespace FluentDeploy.Commands.Validation
{
    public class ConstResultCommandExecutionValidator : ICommandExecutionValidator
    {
        private readonly CommandExecutionValidationResult _executionValidationResult;

        public ConstResultCommandExecutionValidator(CommandExecutionValidationResult executionValidationResult)
        {
            _executionValidationResult = executionValidationResult;
        }

        public CommandExecutionValidationResult Validate(CommandExecutionResult result)
        {
            return _executionValidationResult;
        }
    }
}