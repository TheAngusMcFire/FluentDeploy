using System.Collections.Generic;
using FluentDeploy.Commands;
using FluentDeploy.Execution;

namespace FluentDeploy.ExecutionEngine
{
    public class LocalExecutor : ICommandAggregator
    {
        public void AddCommands(List<BaseCommand> commands)
        {
            throw new System.NotImplementedException();
        }

        public void AddCommand(BaseCommand commands)
        {
            throw new System.NotImplementedException();
        }

        public void Execute()
        {
            
        }
    }
}