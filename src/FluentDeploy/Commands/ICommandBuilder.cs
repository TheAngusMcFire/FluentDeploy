using System.Collections.Generic;
using FluentDeploy.Execution;

namespace FluentDeploy.Commands
{
    public interface ICommandBuilder
    {
        List<BaseCommand> BuildCommands();

        void BuildCommands(ICommandAggregator commandAggregator);
    }
}