using FluentDeploy.Commands;
using FluentDeploy.ExecutionEngine.Interfaces;

namespace FluentDeploy.Extentions
{
    public static class CommandAggregatorExtensions
    {
        public static ICommandExecutor AsRoot(this ICommandExecutor executor)
        {
            executor.ExecuteCommand(CommandStore.AsRootCommand());
            return executor;
        }
        
        public static ICommandExecutor AsUser(this ICommandExecutor executor)
        {
            executor.ExecuteCommand(CommandStore.AsUserCommand());
            return executor;
        }
    }
}