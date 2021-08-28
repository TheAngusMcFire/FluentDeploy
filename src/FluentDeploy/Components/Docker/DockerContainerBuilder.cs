using System;
using System.Collections.Generic;
using System.Linq;
using FluentDeploy.Commands;
using FluentDeploy.Components.Docker.DockerApi;
using FluentDeploy.Components.Docker.DockerApi.Model;
using FluentDeploy.ExecutionUtils.Interfaces;
using Serilog;

namespace FluentDeploy.Components.Docker
{
    public class DockerContainerBuilder : BaseCommandBuilder<DockerContainerBuilder>
    {
        private readonly string _name;
        private readonly string _image;
        private readonly Dictionary<string, List<PortBinding>> _portMapping = new();
        private readonly List<string> _networks = new(); 
        private readonly List<(string, string, string)> _mounts = new();
        private readonly List<(string, string)> _env = new();
        private readonly ILogger _logger;
        private List<string> _commands = new();
        private List<string> _entrypoint = new();
        private bool _started;
        private bool _restart;
        private bool _forcePullImage;

        public DockerContainerBuilder(string name, string image)
        {
            _name = name;
            _image = image;
            _logger = Log.ForContext<DockerContainerBuilder>();
        }

        public DockerContainerBuilder AddMount(string source, string destination, string options = null)
        {
            var opt = options != null ? $":{options}" : null;
            _mounts.Add((source, destination, opt));
            return this;
        }
        
        public DockerContainerBuilder AddPortMapping(string dockerPort, string hostIp, int hostPort)
        {   
            if(!_portMapping.ContainsKey(dockerPort))
                _portMapping.Add(dockerPort, new List<PortBinding>());
            
            _portMapping[dockerPort].Add(new PortBinding()
            {
                HostIp = hostIp,
                HostPort = hostPort.ToString()
            });
            
            return this;
        }

        public DockerContainerBuilder AddNetwork(string networkName) => 
            FluentExec(() => _networks.Add(networkName));

        public DockerContainerBuilder Started(bool started = true) => 
            FluentExec(() => _started = started);

        public DockerContainerBuilder Restart(bool restart = true) => 
            FluentExec(() => _restart = restart);
        
        public DockerContainerBuilder ForcePullImage(bool forcePull = true) => 
            FluentExec(() => _forcePullImage = forcePull);
        
        public DockerContainerBuilder ForcePullImage(string name, string value) => 
            FluentExec(() => _env.Add((name, value)));

        public DockerContainerBuilder Commands(params string[] cmds) => 
            FluentExec(() => _commands.AddRange(cmds));
        
        private bool CheckIfContainerNeedsUpdating(ContainerInspectResponse container)
        {
            return true;
        }

        private void CreateDockerContainer(DockerApi.DockerApi api)
        {
            var endpoints = new Dictionary<string, EndpointSettings>();
            var networks = api.GetNetworks();
            _networks.ForEach(x => endpoints.Add(x, new EndpointSettings()
            {
                NetworkID = networks.First(y => y.Name == x).Id
            }));
        
            var config = new ContainerConfig()
            {
                Image = _image,
                Cmd = _commands,
                Entrypoint = _entrypoint,
                Env = _env.Select(x => $"{x.Item1}={x.Item2}").ToList(),
                NetworkingConfig = new NetworkingConfig()
                {
                    EndpointsConfig = endpoints
                },
                HostConfig = new HostConfig()
                {
                    Binds = _mounts.Select(x => $"{x.Item1}:{x.Item2}{x.Item3}".Trim()).ToList(),
                    PortBindings = _portMapping
                }
            };

            var resp = api.CreateContainer(_name, config);

            if (_started)
                api.StartContainer(_name);
        }

        protected override void Execute(IExecutionContext executor)
        {
            var api = new DockerApi.DockerApi(new CurlDockerHttpClient(executor) {Timeout = 600});
            
            var targetImage = api.InspectImage(_image); 

            if (targetImage == null || _forcePullImage)
            {
                _logger.Debug(api.PullImage(_image));
                api.InspectImage(_image); 
            }
            
            var container = api.InspectContainer(_name);

            if (container == null)
            {
                CreateDockerContainer(api);

                if (_restart)
                {
                    api.RestartContainer(_name);
                }
            }
            else
            {
                if (CheckIfContainerNeedsUpdating(container))
                {
                    var tmpName = $"{_name}_tmp_{DateTime.Now.Ticks}";
                    api.RenameContainer(_name, tmpName);
                    CreateDockerContainer(api);
                    api.StopContainer(tmpName);
                    
                    if(_started && !_restart)
                        api.StartContainer(_name);
                    
                    api.DeleteContainer(tmpName);
                }
                
                if (_restart)
                {
                    api.RestartContainer(_name);
                }
            }
        }
    }
}