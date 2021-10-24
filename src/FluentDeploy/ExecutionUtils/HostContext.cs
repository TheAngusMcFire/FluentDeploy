using FluentDeploy.Commands;
using FluentDeploy.DistributionVariants;
using FluentDeploy.ExecutionEngine.ExecutionResults;
using FluentDeploy.ExecutionEngine.Interfaces;
using FluentDeploy.ExecutionUtils.Interfaces;
using FluentDeploy.HostLogic;

namespace FluentDeploy.ExecutionUtils
{
    public class HostContext : IExecutionContext
    {
        private readonly ICommandExecutor _commandExecutor;

        public HostContext(Host host, ICommandExecutor commandExecutor)
        {
            _commandExecutor = commandExecutor;
            Host = host;
        }
        
        public CommandExecutionResult ExecuteCommand(BaseCommand cmd)
        {
            return _commandExecutor.ExecuteCommand(cmd);
        }

        public Host Host { get;}
        public string UserName { get; set; }
        public bool PackageManagerMirrorsUpdated { get; set; }
        public int UserId { get; set; }
        public int UserGroupId { get; set; }
        public string SystemTmpPath => "/tmp";
        public IDistributionVariant DistributionVariant { get; set; }
    }
}