using System.Collections.Generic;

namespace FluentDeploy.Commands
{
    public interface ICommandBuilder
    {
        List<BaseCommand> BuildCommands();

        void BuildCommands(ICommandAggregator commandAggregator);
    }
}