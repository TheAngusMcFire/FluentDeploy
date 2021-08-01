using System;
using FluentDeploy.ExecutionEngine;
using FluentDeploy.ExecutionEngine.ExecutionResults;

namespace FluentDeploy.Commands.Validation
{
    public class ReturnCodeConsoleCommandValidator : ICommandExecutionValidator
    {
        private readonly int _expectedReturnCode;

        public ReturnCodeConsoleCommandValidator(int expectedReturnCode)
        {
            _expectedReturnCode = expectedReturnCode;
        }

        public CommandExecutionValidationResult Validate(CommandExecutionResult result)
        {
            if (!(result is ConsoleCommandExecutionResult res))
            {
                throw new InvalidOperationException();
            }
            
            var success = res.ReturnCode == _expectedReturnCode;

            var msg = success switch
            {
                true => null,
                false => $"Error expected return code: {_expectedReturnCode} bot got: {res.ReturnCode}"
            };

            return new CommandExecutionValidationResult()
            {
                WasSuccessful = success,
                ErrorMessage = msg
            };
        }
    }
}