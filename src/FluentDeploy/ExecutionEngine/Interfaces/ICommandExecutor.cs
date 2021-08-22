using FluentDeploy.Commands;
using FluentDeploy.ExecutionEngine.ExecutionResults;

namespace FluentDeploy.ExecutionEngine.Interfaces
{
    public interface ICommandExecutor
    {
        CommandExecutionResult ExecuteCommand(BaseCommand cmd);
    }
}