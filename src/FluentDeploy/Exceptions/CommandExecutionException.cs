namespace FluentDeploy.Exceptions
{
    public class CommandExecutionException : FluentDeployException
    {
        public CommandExecutionException(string? message) : base(message)
        {
        }
    }
}