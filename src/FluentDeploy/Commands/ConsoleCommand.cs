using System.IO;
using FluentDeploy.Commands.Validation;

namespace FluentDeploy.Commands
{
    public class ConsoleCommand : BaseCommand
    {
        public string ExecutableName { get; set; }
        public string[] Arguments { get; set; }
        public Stream StandardInput { get; set; }
        public override ICommandExecutionValidator Validator { get; set; }

        public bool WithRoot { get; set; } = false;

        public static ConsoleCommand AsRoot(string executableName) => new ()
            { ExecutableName = executableName, WithRoot = true };

        public static ConsoleCommand AsUser(string executableName) => new ()
            { ExecutableName = executableName, WithRoot = true };

        
        public ConsoleCommand WithArguments(string[] arguments)
        {
            Arguments = arguments;
            return this;
        }
        
        public ConsoleCommand WithArgument(string argument)
        {
            Arguments = new [] { argument };
            return this;
        }

        public ConsoleCommand DefaultValidator()
        {
            Validator = new ReturnCodeCommandValidator(0);
            return this;
        }
    }
}