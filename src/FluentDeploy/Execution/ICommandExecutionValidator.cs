using System.IO;

namespace FluentDeploy.Execution
{
    public interface ICommandExecutionValidator
    {
        void Validate(int retCode, Stream stdOut);
    }
}