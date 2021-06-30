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
    }
}