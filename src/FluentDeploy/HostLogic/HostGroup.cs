using System.Collections.Generic;

namespace FluentDeploy.HostLogic
{
    public class HostGroup
    {
        public string GroupName { get; set; }
        public List<Host> Hosts { get; set; }
    }
}