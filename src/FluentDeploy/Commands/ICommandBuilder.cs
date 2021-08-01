using System.Collections.Generic;
using FluentDeploy.ExecutionUtils;
using FluentDeploy.ExecutionUtils.Interfaces;

namespace FluentDeploy.Commands
{
    public interface ICommandBuilder
    {
        ExecutionUnit BuildCommands(IHostInfo hostInfo);

        void BuildCommands(IExecutionContext executionContext);
    }
}