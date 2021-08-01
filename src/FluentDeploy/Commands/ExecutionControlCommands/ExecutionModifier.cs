using FluentDeploy.Commands.Validation;

namespace FluentDeploy.Commands.ExecutionControlCommands
{
    public enum ExecutionModifierType
    {
        RunAsRoot,
        RunAsUser,
        ResetPrivilegeChange,
        PackageManagerUpdated
    }

    public class ExecutionModifier : BaseCommand
    {
        public ExecutionModifierType ModifierType { get; set; }
        public override ICommandExecutionValidator Validator 
            => new ConstResultCommandExecutionValidator(CommandExecutionValidationResult.SuccessResult);
    }
}