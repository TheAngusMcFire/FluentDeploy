using System;
using FluentDeploy.ExecutionEngine;

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
                false => $"Error expected return code: {res.ReturnCode} bot got: {_expectedReturnCode}"
            };

            return new CommandExecutionValidationResult()
            {
                WasSuccessful = success,
                ErrorMessage = msg
            };
        }
    }
}