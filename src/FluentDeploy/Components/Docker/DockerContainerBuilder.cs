using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using FluentDeploy.Commands;
using FluentDeploy.Components.Docker.DockerApi;
using FluentDeploy.Components.Docker.DockerApi.Model;
using FluentDeploy.Exceptions;
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
        private readonly List<string> _mounts = new();
        private readonly List<string> _env = new();
        private readonly ILogger _logger;
        private readonly List<string> _commands = null;
        private readonly List<string> _capabilities = new();
        private readonly List<string> _entrypoint = new();
        private string _authToken;
        private string _hostname;
        private bool _started;
        private bool _restart;
        private bool _forcePullImage;
        private DockerApi.DockerApi _api;

        public DockerContainerBuilder(IDockerHttpClient client, string name, string image)
        {
            _name = name;
            _image = image;
            _logger = Log.ForContext<DockerContainerBuilder>();
            _api = new DockerApi.DockerApi(client);
        }

        public DockerContainerBuilder(string name, string image)
        {
            _name = name;
            _image = image;
            _logger = Log.ForContext<DockerContainerBuilder>();
        }
        
        public DockerContainerBuilder AddMount(string source, string destination, string options = null)
        {
            AddMount($"{source}:{destination}", options);
            return this;
        }
        
        public DockerContainerBuilder AddMount(string mount, string options = null)
        {
            var opt = options != null ? $":{options}" : null;
            AddMount($"{mount}{opt}");
            return this;
        }
        
        public DockerContainerBuilder AddMount(string mountString)
        {
            _mounts.Add(mountString);
            return this;
        }
        
        /// <summary>
        /// in the format: 443/tcp
        /// </summary>
        /// <param name="dockerPort"></param>
        /// <param name="hostIp"></param>
        /// <param name="hostPort"></param>
        /// <returns></returns>
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

        public DockerContainerBuilder AddCapability(string capabilityName) =>
            FluentExec(() => _capabilities.Add(capabilityName));
        
        public DockerContainerBuilder Hostname(string hostname) => 
            FluentExec(() => _hostname = hostname);
        
        public DockerContainerBuilder RegistryAuthToken(string token) => 
            FluentExec(() => _authToken = token);
        
        public DockerContainerBuilder AddNetwork(string networkName) => 
            FluentExec(() => _networks.Add(networkName));

        public DockerContainerBuilder Started(bool started = true) => 
            FluentExec(() => _started = started);

        public DockerContainerBuilder Restart(bool restart = true) => 
            FluentExec(() => _restart = restart);
        
        public DockerContainerBuilder ForcePullImage(bool forcePull = true) => 
            FluentExec(() => _forcePullImage = forcePull);
        
        public DockerContainerBuilder AddEnvironmentVar(string name, string value) => 
            FluentExec(() => _env.Add($"{name}={value}"));

        public DockerContainerBuilder Commands(params string[] cmds) => 
            FluentExec(() => _commands.AddRange(cmds));

        private bool CheckIfListsNeedUpdating(List<string> remote, List<string> local, bool capabilities)
        {
            if (remote == null && local != null)
                return true;

            if (remote == null && local == null)
                return false;
            
            if (remote != null && local == null && !capabilities)
                return false;

            if (remote != null && local == null && capabilities)
                return true;
            
            return remote.SequenceEqual(local);
        }

        private bool CheckIfContainerNeedsUpdating(ContainerInspectResponse container, string imageId)
        {
            if (container.Image != imageId)
                return true;
            
            if(!_portMapping.All(x => container.HostConfig.PortBindings.Any(y => y.Key == x.Key && y.Value.SequenceEqual(x.Value))))
                return true;
            
            if(!_networks.All(x => container.NetworkSettings.Networks.Any(y => y.Key == x)))
                return true;
            
            if(!_mounts.All(x => container.HostConfig.Binds.Contains(x)))
                return true;

            if(!_env.All(x => container.Config.Env.Contains(x)))
                return true;
            
            if(CheckIfListsNeedUpdating(container.Config.Cmd, _commands, false))
                return true;

            if(!_entrypoint.All( x => container.Config.Entrypoint.Contains(x)))
                return true;

            if (CheckIfListsNeedUpdating(container.HostConfig.Capabilities, _capabilities, true))
                return true;
            
            return false;
        }

        private string CreateDockerContainer(DockerApi.DockerApi api)
        {
            var endpoints = new Dictionary<string, EndpointSettings>();
            var networks = api.GetNetworks();

            if (_networks.Count != 0)
            {
                var nw = _networks.First();
                endpoints.Add(nw, new EndpointSettings()
                {
                    NetworkID = networks.First(y => y.Name == nw).Id
                });
            }
        
            var config = new ContainerConfig()
            {
                Hostname = _hostname,
                Image = _image,
                Cmd = _commands,
                Entrypoint = _entrypoint.Count == 0 ? null : _entrypoint,
                Env = _env,
                NetworkingConfig = new NetworkingConfig()
                {
                    EndpointsConfig = endpoints
                },
                HostConfig = new HostConfig()
                {
                    Binds = _mounts,
                    PortBindings = _portMapping,
                    Capabilities = _capabilities.Count == 0 ? null : _capabilities
                }
            };

            var newId = api.CreateContainer(_name, config);

            if (_networks.Count > 1)
            {
                foreach (var x in _networks.Skip(1))
                {
                    var nwi = networks.FirstOrDefault(y => y.Name == x);

                    if (nwi is null)
                    {
                        throw new FluentDeployException($"Network {x}  not found");
                    }

                    api.ConnectToNetwork(newId, nwi.Id);
                }
            }

            return newId;
        }

        public void Execute()
        {
            Execute(null);
        }

        protected override void Execute(IExecutionContext context)
        {
            if (_api == null)
            {
                _api = new DockerApi.DockerApi(new CurlDockerHttpClient(context) {Timeout = 600});
            }
            
            var targetImage = _api.InspectImage(_image); 

            if (targetImage == null || _forcePullImage)
            {
                _logger.Debug(_api.PullImage(_image, _authToken));
                targetImage = _api.InspectImage(_image);

                if (targetImage is null)
                {
                    throw new FluentDeployException($"Image {_image} does not exist");
                }
            }
            
            var container = _api.InspectContainer(_name);

            if (container == null)
            {
                var id = CreateDockerContainer(_api);
                
                if (_started)
                    _api.StartContainer(id);
            }
            else
            {
                if (CheckIfContainerNeedsUpdating(container, targetImage.Id) || _forcePullImage)
                {
                    var tmpName = $"{_name}_tmp_{DateTime.Now.Ticks}";
                    _api.RenameContainer(_name, tmpName);
                    CreateDockerContainer(_api);
                    _api.StopContainer(tmpName);

                    if(_started && !_restart)
                        _api.StartContainer(_name);

                    _api.DeleteContainer(tmpName);
                }
            }

            if (_restart)
            {
                _api.RestartContainer(_name);
            }
        }
    }
}