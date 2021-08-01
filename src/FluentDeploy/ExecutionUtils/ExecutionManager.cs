using FluentDeploy.ExecutionUtils.Interfaces;

namespace FluentDeploy.ExecutionUtils
{
    public class ExecutionManager
    {
        public ICommandAggregator Local { get; set; }
        public ICommandAggregator Remote { get; set; }
    }
}