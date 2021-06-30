using System;
using System.Collections.Generic;

namespace FluentDeploy.Config
{
    public class HostConfig
    {
        public HostInformation HostInfo { get; set; }
        public Dictionary<string, string> ConfigValues { get; set; }

        public string GetConfigString(string key) 
            => ConfigValues.GetValueOrDefault(key);
        
        public int GetConfigInt(string key) 
            => Convert.ToInt32(ConfigValues.GetValueOrDefault(key));
    }
}