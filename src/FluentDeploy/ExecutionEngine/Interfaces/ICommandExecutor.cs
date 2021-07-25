using FluentDeploy.Commands;

namespace FluentDeploy.ExecutionEngine.Interfaces
{
    public interface ICommandExecutor
    {
        CommandExecutionResult ExecuteCommand(BaseCommand cmd);
    }
}