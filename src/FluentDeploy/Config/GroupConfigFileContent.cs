using System.Collections.Generic;

namespace FluentDeploy.Config
{
    public class GroupConfigFileContent
    {
        public string GroupName { get; set; }
        public List<string> Hosts { get; set; }
    }
}