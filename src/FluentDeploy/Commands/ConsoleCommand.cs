using System;
using System.IO;
using FluentDeploy.Commands.Validation;

namespace FluentDeploy.Commands
{
    public class ConsoleCommand : BaseCommand
    {
        
        public string ExecutableName { get; set; }
        public string[] Arguments { get; set; }
        public int Timeout { get; set; } = 60;
        public string WorkingDir { get; set; }
        public bool ReadAllOutput { get; set; }
        public Stream StandardInput { get; set; }
        private ICommandExecutionValidator _validator = new ReturnCodeConsoleCommandValidator(0);

        public static ConsoleCommand Exec(string executableName, params string[] arguments) => new ()
            { ExecutableName = executableName, Arguments = arguments};

        private ConsoleCommand()
        {

        }

        public ConsoleCommand WorkingDirectory(string workingDir)
        {
            WorkingDir = workingDir;
            return this;
        }
        
        public ConsoleCommand WithArguments(params string[] arguments)
        {
            Arguments = arguments;
            return this;
        }
        
        public ConsoleCommand WithTimeout(int timeout)
        {
            Timeout = timeout;
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