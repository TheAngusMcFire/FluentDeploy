using System.Linq;
using FluentDeploy.Commands;
using FluentDeploy.Components.Docker.DockerApi;
using FluentDeploy.ExecutionUtils.Interfaces;

namespace FluentDeploy.Components.Docker
{
    public class DockerNetworkBuilder : BaseCommandBuilder<DockerNetworkBuilder>
    {
        private readonly string _name;

        public DockerNetworkBuilder(string name)
        {
            _name = name;
        }

        protected override void Execute(IExecutionContext executor)
        {
            var api = new DockerApi.DockerApi(new CurlDockerHttpClient(executor));

            if (api.GetNetworks().Any(x => x.Name == _name))
                return;
            
            api.CreateNetwork(_name);
        }
    }
}