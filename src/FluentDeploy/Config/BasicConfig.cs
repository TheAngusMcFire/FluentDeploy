using System.Collections.Generic;
using System.Linq;
using FluentDeploy.Consts;
using Microsoft.Extensions.Configuration;

namespace FluentDeploy.Config
{
    public class BasicConfig
    {
        private readonly Dictionary<string, Dictionary<string, IConfigurationRoot>> _groupHostConfig;
        private readonly Dictionary<string, IConfigurationRoot> _hostConfig;

        public BasicConfig(Dictionary<string, Dictionary<string, IConfigurationRoot>> groupHostConfig, Dictionary<string, IConfigurationRoot> hostConfig)
        {
            _groupHostConfig = groupHostConfig;
            _hostConfig = hostConfig;
        }

        public HostConfig GetHostConfig(string hostName)
        {
            var hostInfo = new HostInformation();
            _hostConfig[hostName].Bind(hostInfo);
            var sshConfigs = new Dictionary<string, SshConfig>();
            _hostConfig[hostName].GetSection("SshKeyConfigs").Bind(sshConfigs);

            return new HostConfig()
            {
                HostInfo = hostInfo,
                Config = _hostConfig[hostName],
                SshConfigs = sshConfigs
            };
        }

        public List<HostConfig> GetGroupHostConfigs(string groupName)
        {
            return _groupHostConfig[groupName].Select(x =>
            {
                var hostInfo = new HostInformation();
                var sshConfigs = new Dictionary<string, SshConfig>();
                x.Value.GetSection("SshKeyConfigs").Bind(sshConfigs);
                x.Value.Bind(hostInfo);
                
                
                return new HostConfig()
                {
                    HostInfo = hostInfo,
                    Config = x.Value,
                    SshConfigs = sshConfigs
                };
            }).ToList();
        }
    }
}