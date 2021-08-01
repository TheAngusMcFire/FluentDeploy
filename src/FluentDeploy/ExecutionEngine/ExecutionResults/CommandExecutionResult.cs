using System;
using FluentDeploy.Commands.Validation;

namespace FluentDeploy.ExecutionEngine.ExecutionResults
{
    public class CommandExecutionResult
    {
        public CommandExecutionValidationResult ValidationResult { get; set; }
        
        public static CommandExecutionResult SuccessResult => new () { ValidationResult = CommandExecutionValidationResult.SuccessResult};

        public virtual void PrintResultData(Action<string> printFunction)
        {
            
        }
    }
}