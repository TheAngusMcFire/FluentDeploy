using System.Drawing;
using FluentDeploy.Commands;
using FluentDeploy.ExecutionEngine.ExecutionResults;
using FluentDeploy.ExecutionUtils.Interfaces;

namespace FluentDeploy.Extentions
{
    public static class ExecutionContextExtensions
    {
        public static ConsoleCommandExecutionResult ExecuteConsoleCommand(this IExecutionContext context,
            ConsoleCommand cmd) => context.ExecuteCommand(cmd) as ConsoleCommandExecutionResult;
    }
}