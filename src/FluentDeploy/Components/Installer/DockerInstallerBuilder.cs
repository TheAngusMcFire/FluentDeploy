using FluentDeploy.Commands;
using FluentDeploy.Components.PackageManagers;
using FluentDeploy.DistributionVariants;
using FluentDeploy.ExecutionUtils.Interfaces;

namespace FluentDeploy.Components.Installer
{
    public class DockerInstallerBuilder : BaseCommandBuilder<DockerInstallerBuilder>
    {
        private readonly bool _withCompose;

        public DockerInstallerBuilder(bool withCompose)
        {
            _withCompose = withCompose;
        }

        protected override void Execute(IExecutionContext executor)
        {
            if (executor.DistributionVariant.DistributionVariantType == DistributionVariantType.Debian)
            {
                AptGet.Install("apt-transport-https", "ca-certificates", "curl", "gnupg", "lsb-release")
                    .ExecuteOn(executor);

                //todo maybe there is a less crappy way to install docker on debian
                //todo add file checks to see if we need to delete files
                //todo build something to check for the newest version
                //todo the key adding and the
                //todo build something to get the release of debian
                executor.ExecuteCommand(ConsoleCommand.Exec(
                    "rm -rf  /usr/share/keyrings/docker-archive-keyring.gpg && curl -fsSL https://download.docker.com/linux/debian/gpg | sudo gpg --batch --dearmor -o /usr/share/keyrings/docker-archive-keyring.gpg"));
                
                executor.ExecuteCommand(ConsoleCommand.Exec(
                    @"echo  ""deb [arch=amd64 signed-by=/usr/share/keyrings/docker-archive-keyring.gpg] https://download.docker.com/linux/debian \
                    $(lsb_release -cs) stable"" | sudo tee /etc/apt/sources.list.d/docker.list > /dev/null"));

                AptGet.Update()
                    .ExecuteOn(executor);

                AptGet.Install("docker-ce", "docker-ce-cli", "containerd.io")
                    .ExecuteOn(executor);

                if (_withCompose)
                {
                    executor.ExecuteCommand(ConsoleCommand.Exec(@"curl -L ""https://github.com/docker/compose/releases/download/1.29.2/docker-compose-$(uname -s)-$(uname -m)"" -o /usr/local/bin/docker-compose"));
                    executor.ExecuteCommand(ConsoleCommand.Exec(@"chmod +x /usr/local/bin/docker-compose"));
                    executor.ExecuteCommand(ConsoleCommand.Exec(@"ln -s /usr/local/bin/docker-compose /usr/bin/docker-compose"));
                }
            }
        }
    }
}