using FluentDeploy.Commands;
using Serilog;

namespace FluentDeploy.ExecutionEngine.Interfaces
{
    public interface IHostCommandExecutor
    {
        CommandExecutionResult ExecuteConsoleCommand(ConsoleCommand cmd, bool asRoot);
    }
}