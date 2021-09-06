using System.Collections.Generic;
using System.Linq;
using FluentDeploy.Consts;

namespace FluentDeploy.Config
{
    public class BasicConfig
    {
        public Dictionary<string, List<HostInformation>> GroupHosts { get; set; }
        public Dictionary<string, Dictionary<string, string>> GroupHostVars { get; set; }

        public List<HostConfig> GetUngroupedHostConfigs() 
            => GetHostConfigsFromGroup(StringConstants.UngroupedHostsGroupName);

        public HostConfig GetUngroupedHostConfig(string hostName) =>
            GetUngroupedHostConfigs().First(x => x.HostInfo.Name == hostName);

        public List<HostConfig> GetHostConfigsFromGroup(string groupName)
        {
            var hosts = new List<HostConfig>();
            var defaultHosts = GroupHosts.GetValueOrDefault(groupName, new List<HostInformation>());

            foreach (var host in defaultHosts)
            {
                var hostVars = GroupHostVars.GetValueOrDefault(groupName);
                hosts.Add(new HostConfig(){HostInfo = host, ConfigValues = hostVars});
            }

            return hosts;
        }
    }
}