using FluentDeploy.Commands;

namespace FluentDeploy.ExecutionUtils.Interfaces
{
    public interface ICommandAggregator<T> : ICommandAggregator
    {
        new T AddCommand(BaseCommand command);

        void ICommandAggregator.AddCommand(BaseCommand command)
        {
            AddCommand(command);
        }
    }
    
    public interface ICommandAggregator
    {
        void AddCommand(BaseCommand command);
    }
}