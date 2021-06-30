using FluentDeploy.Commands.ExecutionControlCommands;

namespace FluentDeploy.Commands
{
    public class CommandStore
    {
        public static ExecutionModifier AsRootCommand() => new ExecutionModifier()
            {ModifierType = ExecutionModifierType.RunAsRoot};
        
        public static ExecutionModifier AsUserCommand() => new ExecutionModifier()
            {ModifierType = ExecutionModifierType.RunAsUser};

        public static ExecutionModifier PackageManagerUpdated() => new ExecutionModifier()
            {ModifierType = ExecutionModifierType.PackageManagerUpdated};
        
        public static ExecutionModifier ResetPrivilegeChange() => new ExecutionModifier()
            {ModifierType = ExecutionModifierType.ResetPrivilegeChange};
    }
}