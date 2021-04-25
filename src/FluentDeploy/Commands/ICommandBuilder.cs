using System.Collections.Generic;
using FluentDeploy.Execution;

namespace FluentDeploy.Commands
{
    public interface ICommandBuilder
    {
        ExecutionUnit BuildCommands(IHostInfo hostInfo);

        void BuildCommands(ICommandContext commandContext);
    }
}