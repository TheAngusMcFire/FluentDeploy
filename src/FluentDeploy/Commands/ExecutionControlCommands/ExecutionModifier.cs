namespace FluentDeploy.Commands.ExecutionControlCommands
{
    public enum ExecutionModifierType
    {
        RunAsRoot,
        RunAsUser,
        PackageManagerUpdated
    }

    public class ExecutionModifier : BaseCommand
    {
        public ExecutionModifierType ModifierType { get; set; }
    }
}