using FluentDeploy.Commands.Validation;

namespace FluentDeploy.ExecutionEngine
{
    public class CommandExecutionResult
    {
        public CommandExecutionValidationResult ValidationResult { get; set; }
        
        public static CommandExecutionResult SuccessResult => new () { ValidationResult = CommandExecutionValidationResult.SuccessResult};
    }
}