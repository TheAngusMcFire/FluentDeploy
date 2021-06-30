using System.Collections.Generic;
using System.Linq;
using FluentDeploy.Consts;

namespace FluentDeploy.Config
{
    public class BasicConfig
    {
        public Dictionary<string, List<HostInformation>> GroupHosts { get; set; }
        public Dictionary<string, Dictionary<string, string>> GroupHostVars { get; set; }

        public List<HostConfig> GetUngroupedHosts() 
            => GetHostsFromGroup(StringConstants.UngroupedHostsGroupName);

        public HostConfig GetUngroupedHost(string hostName) =>
            GetUngroupedHosts().First(x => x.HostInfo.Name == hostName);

        public List<HostConfig> GetHostsFromGroup(string groupName)
        {
            var hosts = new List<HostConfig>();
            var defaultHosts = GroupHosts.GetValueOrDefault(groupName, new List<HostInformation>());

            foreach (var host in defaultHosts)
            {
                var hostVars = GroupHostVars.GetValueOrDefault(host.Name);
                hosts.Add(new HostConfig(){HostInfo = host, ConfigValues = hostVars});
            }

            return hosts;
        }
    }
}