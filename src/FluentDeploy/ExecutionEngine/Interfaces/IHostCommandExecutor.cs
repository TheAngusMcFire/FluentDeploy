using FluentDeploy.Commands;
using Serilog;

namespace FluentDeploy.ExecutionEngine.Interfaces
{
    public interface IHostCommandExecutor
    {
        ConsoleCommandExecutionResult ExecuteConsoleCommand(ConsoleCommand cmd, bool asRoot);
    }
}