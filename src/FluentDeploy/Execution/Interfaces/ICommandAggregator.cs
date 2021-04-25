using System.Collections.Generic;
using FluentDeploy.Commands;

namespace FluentDeploy.Execution
{
    public interface ICommandAggregator
    {
        void AddCommand(BaseCommand command);
    }
}