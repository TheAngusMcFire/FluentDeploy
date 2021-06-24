namespace FluentDeploy.Execution
{
    public interface ICommandContext<T> : IHostInfo, ICommandAggregator<T>
    {
        
    }
    
    public interface ICommandContext : IHostInfo, ICommandAggregator
    {
        
    }
}