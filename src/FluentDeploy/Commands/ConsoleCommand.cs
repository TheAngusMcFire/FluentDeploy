using System.IO;
using FluentDeploy.Commands.Validation;

namespace FluentDeploy.Commands
{
    public class ConsoleCommand : BaseCommand
    {
        public string ExecutableName { get; set; }
        public string[] Arguments { get; set; }
        public Stream StandardInput { get; set; }
        private ICommandExecutionValidator _validator = new ReturnCodeConsoleCommandValidator(0);

        public static ConsoleCommand Exec(string executableName) => new ()
            { ExecutableName = executableName };

        private ConsoleCommand()
        {

        }

        public ConsoleCommand WithArguments(params string[] arguments)
        {
            Arguments = arguments;
            return this;
        }

        public ConsoleCommand WithValidator(ICommandExecutionValidator validator)
        {
            _validator = validator;
            return this;
        }

        public override ICommandExecutionValidator Validator => _validator;
    }
}