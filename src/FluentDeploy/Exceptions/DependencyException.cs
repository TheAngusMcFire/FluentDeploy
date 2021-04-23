namespace FluentDeploy.Exceptions
{
    public class DependencyException : FluentDeployException
    {
        public DependencyException(string? message) : base(message)
        {
        }
    }
}