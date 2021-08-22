using FluentDeploy.Commands.ExecutionControlCommands;

namespace FluentDeploy.Commands
{
    public class CommandStore
    {
        public static ExecutionModifier AsRootCommand() => new ()
            {ModifierType = ExecutionModifierType.RunAsRoot};
        
        public static ExecutionModifier AsUserCommand() => new ()
            {ModifierType = ExecutionModifierType.RunAsUser};

        public static ExecutionModifier PackageManagerUpdated() => new ()
            {ModifierType = ExecutionModifierType.PackageManagerUpdated};
        
        public static ExecutionModifier ResetPrivilegeChange() => new ()
            {ModifierType = ExecutionModifierType.ResetPrivilegeChange};
    }
}