using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;

namespace FluentDeploy.Config
{
    public class HostConfig
    {
        public Dictionary<string, SshConfig> SshConfigs { get; set; }
        public HostInformation HostInfo { get; set; }
        public IConfigurationRoot Config { get; set; }

        public string GetConfigString(string key) => 
            Config.GetSection($"Vars:{key}").Value;

        public int GetConfigInt(string key) 
            => Convert.ToInt32(GetConfigString(key));
        
        public void BindObject(string key, object obj) =>
            Config.GetSection($"Vars:{key}").Bind(obj);
    }
}