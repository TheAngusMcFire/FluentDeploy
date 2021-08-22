using FluentDeploy.Commands;
using FluentDeploy.ExecutionEngine.ExecutionResults;
using Serilog;

namespace FluentDeploy.ExecutionEngine.Interfaces
{
    public interface IHostCommandExecutor
    {
        ConsoleCommandExecutionResult ExecuteConsoleCommand(ConsoleCommand cmd, bool asRoot);
        FileOperationExecutionResult CreateDirectory(FileOperationCommand cmd, bool asRoot);
    }
}