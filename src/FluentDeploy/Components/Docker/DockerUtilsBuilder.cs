using System;
using FluentDeploy.Commands;
using FluentDeploy.Components.Docker.DockerApi;
using FluentDeploy.ExecutionUtils.Interfaces;

namespace FluentDeploy.Components.Docker
{
    public enum DockerOperation
    {
        None,
        StopContainer,
        StartContainer,
        RenameContainer,
        RemoveContainer,
        PruneImages
    }

    public class DockerUtilsBuilder : BaseCommandBuilder<DockerUtilsBuilder>
    {
        private readonly DockerOperation _dockerOperation;
        private bool _danglingImages = true;
        private string _newContainerName;
        private string _dockerContainerName;

        public DockerUtilsBuilder(DockerOperation dockerOperation)
        {
            _dockerOperation = dockerOperation;
        }

        public DockerUtilsBuilder SetContainerName(string containerName) =>
            FluentExec(() => _dockerContainerName = containerName);
        
        public DockerUtilsBuilder SetNewContainerName(string containerName) =>
            FluentExec(() => _newContainerName = containerName);

        public DockerUtilsBuilder OnlyDanglingImages(bool dangling) =>
            FluentExec(() => _danglingImages = _danglingImages);

        protected override void Execute(IExecutionContext executor)
        {
            var api = new DockerApi.DockerApi(new CurlDockerHttpClient(executor) {Timeout = 600});

            switch (_dockerOperation)
            {
                case DockerOperation.StopContainer:
                    api.StopContainer(_dockerContainerName);
                    break;
                case DockerOperation.StartContainer:
                    api.StartContainer(_dockerContainerName);
                    break;
                case DockerOperation.RenameContainer:
                    api.RenameContainer(_dockerContainerName, _newContainerName);
                    break;
                case DockerOperation.RemoveContainer:
                    api.DeleteContainer(_dockerContainerName);
                    break;
                case DockerOperation.PruneImages:
                    api.PruneImages(_danglingImages);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}