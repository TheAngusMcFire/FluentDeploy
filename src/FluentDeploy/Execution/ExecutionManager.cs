namespace FluentDeploy
{
    public class ExecutionManager
    {
        public ICommandAggregator Local { get; set; }
        public ICommandAggregator Remote { get; set; }
    }
}