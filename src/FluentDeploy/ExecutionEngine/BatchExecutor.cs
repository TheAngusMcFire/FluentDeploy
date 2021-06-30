using System.Collections.Generic;
using FluentDeploy.HostLogic;
using Serilog;

namespace FluentDeploy.ExecutionEngine
{
    public class BatchExecutor
    {
        private readonly ILogger _logger = Log.ForContext<BatchExecutor>(); 
        
        public void ExecuteHosts(List<Host> hosts)
        {

        }
        
        public void ExecuteHosts(HostGroup hostGroup)
        {
            _logger.Information($"Execute hosts from Group {hostGroup.GroupName}");
            ExecuteHosts(hostGroup.Hosts);
        }
    }
}