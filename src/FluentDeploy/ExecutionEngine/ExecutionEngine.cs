using Microsoft.Extensions.Logging;

namespace FluentDeploy.ExecutionEngine
{
    public class ExecutionEngine
    {
        private readonly ILogger _logger;

        public ExecutionEngine(ILogger logger)
        {
            _logger = logger;
        }
        
        
        
    }
}