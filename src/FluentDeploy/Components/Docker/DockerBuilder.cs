using System.Collections.Generic;
using FluentDeploy.Commands;
using FluentDeploy.Components.Docker;

namespace FluentDeploy.Components
{
    public class DockerBuilder
    {
        public static DockerRunCreateBuilder Run(string image, string containerName = null) =>
            new DockerRunCreateBuilder(image, containerName)
                .Run();

        public static DockerRunCreateBuilder RunDetached(string image, string containerName = null) =>
            new DockerRunCreateBuilder(image, containerName)
                .Run().Detached();

        public static DockerRunCreateBuilder Create(string image, string containerName = null) =>
            new DockerRunCreateBuilder(image, containerName);

        public static DockerBuildPushTagImageBuilder Build(string dockerDir, string localImageName,
            string dockerFile = "Dockerfile") =>
            new DockerBuildPushTagImageBuilder()
                .BuildImage(dockerDir, localImageName, dockerFile);
    }
}