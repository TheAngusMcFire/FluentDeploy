namespace FluentDeploy.Commands.Validation
{
    public class CommandExecutionValidationResult
    {
        public bool WasSuccessful { get; set; }
        public string ErrorMessage { get; set; }

        public static CommandExecutionValidationResult SuccessResult => new() { WasSuccessful = true };
    }
}