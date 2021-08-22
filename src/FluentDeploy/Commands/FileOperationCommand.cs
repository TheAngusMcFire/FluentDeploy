using FluentDeploy.Commands.Validation;
using FluentDeploy.Enums;

namespace FluentDeploy.Commands
{
    public class FileOperationCommand : BaseCommand
    {
        public FileOperationType FileOperationType { get; set; }
        public string Source { get; set; }
        public string Destination { get; set; }
        public string FileContent { get; set; }
        public int UserId { get; set; }
        public int GroupId { get; set; }
        public short Permissions { get; set; }

        public override ICommandExecutionValidator Validator =>
            new ConstResultCommandExecutionValidator(CommandExecutionValidationResult.SuccessResult);
    }
}