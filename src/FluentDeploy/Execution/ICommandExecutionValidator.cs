using System.IO;

namespace FluentDeploy
{
    public interface ICommandExecutionValidator
    {
        void Validate(int retCode, Stream stdOut);
    }
}