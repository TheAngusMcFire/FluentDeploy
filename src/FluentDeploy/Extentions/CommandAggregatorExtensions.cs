using FluentDeploy.Commands;
using FluentDeploy.Execution;

namespace FluentDeploy.Extentions
{
    public static class CommandAggregatorExtensions
    {
        public static ICommandAggregator AsRoot(this ICommandAggregator aggregator)
        {
            aggregator.AddCommand(CommandStore.AsRootCommand());
            return aggregator;
        }
        
        public static ICommandAggregator AsUser(this ICommandAggregator aggregator)
        {
            aggregator.AddCommand(CommandStore.AsUserCommand());
            return aggregator;
        }
    }
}