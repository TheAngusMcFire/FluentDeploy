using FluentDeploy.Commands;
using FluentDeploy.ExecutionEngine.ExecutionResults;
using FluentDeploy.ExecutionUtils.Interfaces;

namespace FluentDeploy.Components.Utils
{
    public static class SystemInformation
    {
        public static string GetKernelName(IExecutionContext context) =>
            (context.ExecuteCommand(ConsoleCommand.Exec("uname").WithArguments("--kernel-name")) as
                ConsoleCommandExecutionResult)?.StdOutText.Trim();
        
        public static string GetArchitecture(IExecutionContext context) =>
            (context.ExecuteCommand(ConsoleCommand.Exec("uname").WithArguments("--machine")) as
                ConsoleCommandExecutionResult)?.StdOutText.Trim();
    }
}