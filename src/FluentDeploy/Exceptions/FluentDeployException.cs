using System;

namespace FluentDeploy.Exceptions
{
    public class FluentDeployException : Exception
    {
        public FluentDeployException(string message) : base(message)
        {
        }
    }
}