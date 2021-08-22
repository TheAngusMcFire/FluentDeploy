namespace FluentDeploy.Exceptions
{
    public class CommandValidationException : FluentDeployException
    {
        public CommandValidationException(string message) : base(message)
        { }
    }
}