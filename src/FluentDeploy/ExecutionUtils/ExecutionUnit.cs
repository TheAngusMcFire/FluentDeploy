using System.Collections.Generic;
using FluentDeploy.Commands;
using FluentDeploy.Commands.Validation;
using FluentDeploy.ExecutionUtils.Interfaces;

namespace FluentDeploy.ExecutionUtils
{
    public class ExecutionUnit : BaseCommand, ICommandAggregator<ExecutionUnit>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public List<BaseCommand> Commands { get; set; }

        public ExecutionUnit AddCommand(BaseCommand command)
        {
            Commands.Add(command);
            return this;
        }
        
        public ExecutionUnit AddDescription(string description)
        {
            Description = description;
            return this;
        }

        public void SaveTo(ICommandAggregator aggregator)
        {
            aggregator.AddCommand(this);
        }

        public static ExecutionUnit WithName(string name)
        {
            return new () { Name = name };
        }

        public override ICommandExecutionValidator Validator { get; }
    }
}