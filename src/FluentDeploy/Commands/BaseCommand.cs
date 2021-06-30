using FluentDeploy.Execution;
using Serilog;

namespace FluentDeploy.Commands
{
    public abstract class BaseCommand
    {
        //public virtual IValidator Validator { get; set; } 

        public virtual bool Validate(ILogger logger)
        {
            return false;
        }
    }
}