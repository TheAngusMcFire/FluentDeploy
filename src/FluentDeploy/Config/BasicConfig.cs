using System.Collections.Generic;

namespace FluentDeploy.Config
{
    public class BasicConfig
    {
        public Dictionary<string, List<HostConfig>> GroupHosts { get; set; }
        public Dictionary<string, Dictionary<string, string>> GroupHostVars { get; set; }
    }
}