using System.Collections.Generic;
using FluentDeploy.Commands;

namespace FluentDeploy.Execution
{
    public interface ICommandAggregator
    {
        void AddCommands(List<BaseCommand> commands);
        void AddCommand(BaseCommand commands);
    }
}