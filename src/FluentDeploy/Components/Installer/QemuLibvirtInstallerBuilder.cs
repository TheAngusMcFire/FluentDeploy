using FluentDeploy.Commands;
using FluentDeploy.Components.PackageManagers;
using FluentDeploy.DistributionVariants;
using FluentDeploy.ExecutionUtils.Interfaces;

namespace FluentDeploy.Components.Installer
{
    public class QemuLibVirtInstallerBuilder : BaseCommandBuilder<QemuLibVirtInstallerBuilder>
    {
        protected override void Execute(IExecutionContext context)
        {
            if (context.DistributionVariant.DistributionVariantType == DistributionVariantType.Debian)
            {
                AptGet.Install("qemu-system", "libvirt-clients", "libvirt-daemon-system", "virtinst", "qemu-utils", "libguestfs-tools")
                    .NoRecommends()
                    .ExecuteOn(context);

                context.ExecuteCommand(ConsoleCommand.Exec("adduser").WithArguments(context.UserName, "libvirt"));
            }
        }
    }
}