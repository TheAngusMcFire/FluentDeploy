using System;
using System.IO;
using System.Net.Http;
using System.Threading;
using FluentDeploy.Commands;
using FluentDeploy.Components.Etc;
using FluentDeploy.Components.PackageManagers;
using FluentDeploy.Components.Utils;
using FluentDeploy.DistributionVariants;
using FluentDeploy.Exceptions;
using FluentDeploy.ExecutionEngine.ExecutionResults;
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

        protected override void Execute(IExecutionContext context)
        {
            if (context.DistributionVariant.DistributionVariantType == DistributionVariantType.Debian)
            {
                var dockerComposeVersion = "1.29.2";
                
                AptGet.Install("apt-transport-https", "ca-certificates", "curl", "gnupg", "lsb-release")
                    .ExecuteOn(context);

                var keyRingPath = "/usr/share/keyrings/docker-archive-keyring.gpg";
                var aptListFilePath = "/etc/apt/sources.list.d/docker.list";

                if (!FileSystem.FileSystem.Exists(keyRingPath, context))
                {
                    Gpg.InstallFromUrl("https://download.docker.com/linux/debian/gpg", keyRingPath)
                        .ExecuteOn(context);
                }

                var releaseName = Debian.GetReleaseName(context);

                FileSystem.FileSystem.File(context, aptListFilePath)
                    .Content($"deb [arch=amd64 signed-by={keyRingPath}] https://download.docker.com/linux/debian {releaseName} stable")
                    .ExecuteOn(context);

                AptGet.Update()
                    .ExecuteOn(context);

                AptGet.Install("docker-ce", "docker-ce-cli", "containerd.io")
                    .ExecuteOn(context);

                if (_withCompose)
                {
                    var client = new HttpClient();

                    var result = client
                        .GetAsync(
                            $"https://github.com/docker/compose/releases/download/{dockerComposeVersion}/docker-compose-{SystemInformation.GetKernelName(context)}-{SystemInformation.GetArchitecture(context)}")
                        .Result;

                    if (!result.IsSuccessStatusCode)
                        throw new FluentDeployException(
                            $"Error invalid return code while downloading docker compose code: {result.StatusCode}");

                    var composeCode = result.Content.ReadAsByteArrayAsync().Result;

                    var dockerCodePath = "/usr/local/bin/docker-compose";
                    
                    if (!FileSystem.FileSystem.Exists(dockerCodePath, context))
                    {
                        FileSystem.FileSystem.File(context, dockerCodePath)
                            .Content(composeCode)
                            .Permissions(751)
                            .ExecuteOn(context);
                    }

                    var dockerComposeSymLinkDest = "/usr/bin/docker-compose";

                    if (!FileSystem.FileSystem.Exists(dockerComposeSymLinkDest, context))
                    {
                        FileSystem.FileSystem
                            .SymbolicLink(dockerCodePath, dockerComposeSymLinkDest, context);
                    }
                }
            }
            else
            {
                throw new NotSupportedException();
            }
        }
    }
}