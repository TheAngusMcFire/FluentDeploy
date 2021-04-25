using System.IO;
using FluentDeploy.Commands.Validation;
using FluentDeploy.Execution;

namespace FluentDeploy.Commands
{
    public class ConsoleCommand : BaseCommand
    {
        public string ExecutableName { get; set; }
        public string[] Arguments { get; set; }
        public Stream StandardInput { get; set; }
        //public override ICommandExecutionValidator Validator { get; set; }

        //public bool WithRoot { get; set; } = false;

        //public static ConsoleCommand AsRoot(string executableName) => new ()
        //    { ExecutableName = executableName, WithRoot = true };
//
        public static ConsoleCommand Exec(string executableName) => new ()
            { ExecutableName = executableName };

        private ConsoleCommand()
        {
            DefaultValidator();
        }

        public ConsoleCommand WithArguments(params string[] arguments)
        {
            Arguments = arguments;
            return this;
        }

        public ConsoleCommand DefaultValidator()
        {
            //Validator = new ReturnCodeCommandValidator(0);
            return this;
        }
    }
}