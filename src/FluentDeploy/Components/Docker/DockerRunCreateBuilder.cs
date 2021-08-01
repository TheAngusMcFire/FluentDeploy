using System.Collections.Generic;
using FluentDeploy.Commands;
using FluentDeploy.ExecutionUtils.Interfaces;

namespace FluentDeploy.Components.Docker
{
    public class DockerRunCreateBuilder
    {
        private List<string> _dockerNetworks = new List<string>();
        private Dictionary<string, string> _fileMappings = new Dictionary<string, string>();
        private Dictionary<string, string> _environmentVariables = new Dictionary<string, string>();
        private Dictionary<int, int> _portForwardingRules = new Dictionary<int, int>();
        private string _imageName;
        private string _containerName;
        private bool _willBeRun;
        private bool _detached;
        private bool _stop;
        private bool _delete;
        
        public DockerRunCreateBuilder(string imageName, string containerName = null)
        {
            _containerName = containerName;
            _imageName = imageName;
        }

        public DockerRunCreateBuilder RunDetached()
        {
            _willBeRun = true;
            _detached = true;
            return this;
        }
        
        public DockerRunCreateBuilder Detached()
        {
            _detached = true;
            return this;
        }
        
        public DockerRunCreateBuilder Run()
        {
            _willBeRun = true;
            return this;
        }
        
        public DockerRunCreateBuilder StopAndDeleteIfRunning()
        {
            _stop = true;
            _delete = true;
            return this;
        }


        public DockerRunCreateBuilder AddNetwork(string networkName)
        {
            _dockerNetworks.Add(networkName);
            return this;
        }
        
        public DockerRunCreateBuilder AddFileMapping(string from, string to)
        {
            _fileMappings.Add(from, to);
            return this;
        }
        
        public DockerRunCreateBuilder AddEnvironmentVariable(string name, string value)
        {
            _environmentVariables.Add(name, value);
            return this;
        }
        
        public DockerRunCreateBuilder AddPortForwarding(int hostPort, int dockerPort)
        {
            _portForwardingRules.Add(hostPort, dockerPort);
            return this;
        }

        public List<ConsoleCommand> BuildCommands()
        {
            return null;
        }
        
        public void BuildCommands(ICommandAggregator commandAggregator)
        {
            //commandAggregator.AddCommands();
        }
    }
}