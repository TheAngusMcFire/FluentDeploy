using FluentDeploy.Commands;
using FluentDeploy.ExecutionEngine.ExecutionResults;
using FluentDeploy.ExecutionUtils.Interfaces;

namespace FluentDeploy.Components.Utils
{
    public static class Debian
    {
        public static string GetReleaseName(IExecutionContext context)
        {
            var result = context.ExecuteCommand(ConsoleCommand.Exec("lsb_release").WithArguments("-cs")) 
                as ConsoleCommandExecutionResult;

            return result.StdOutText.Trim();
        }
    }
}