namespace FluentDeploy.Commands.ExecutionControlCommands
{
    public enum ExecutionModifierType
    {
        RunAsRoot,
        RunAsUser
    }

    public class ExecutionModifier : BaseCommand
    {
        public ExecutionModifierType ModifierType { get; set; }
    }
}