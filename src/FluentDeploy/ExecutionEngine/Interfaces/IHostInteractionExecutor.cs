using FluentDeploy.Commands;
using Serilog;

namespace FluentDeploy.ExecutionEngine.Interfaces
{
    public interface IHostInteractionExecutor
    {
        CommandExecutionResult ExecuteConsoleCommand(ConsoleCommand cmd, bool asRoot);
    }
}