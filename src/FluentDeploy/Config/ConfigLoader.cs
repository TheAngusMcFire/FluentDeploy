using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection.Metadata;
using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Configuration;
using YamlDotNet.Core;
using YamlDotNet.Core.Events;
using YamlDotNet.Serialization;

namespace FluentDeploy.Config
{
    public class ConfigLoader
    {   
        private Dictionary<string, Dictionary<string, IConfigurationRoot>> _groupHostConfig = new ();
        private Dictionary<string, IConfigurationRoot> _hostConfig = new ();
        private List<(string path, string passphrase)> _secretFiles = new ();
        private List<string> _configFiles = new();
        
        public ConfigLoader AddConfigDirectory(string directoryPath)
        {
            var files = Directory.GetFiles(directoryPath)
                .Where(x => x.EndsWith("Host.yaml") || x.EndsWith("Group.yaml") || x.EndsWith(".yaml"))
                .Select(x => new FileInfo(x).FullName);
            _configFiles.AddRange(files);
            return this;
        }

        public ConfigLoader AddSecretFiles(string path, string passphrase)
        {
            _secretFiles.Add((path, passphrase));
            return this;
        }

        public BasicConfig Build()
        {
            var groupConfigFiles = _configFiles
                .Where(x => x.ToLower().Contains("group"))
                .Select(x =>
                {
                    var obj = new DeserializerBuilder()
                        .IgnoreUnmatchedProperties()
                        .Build().Deserialize<GroupConfigFileContent>(File.ReadAllText(x));
                    return (path : x, content : obj);
                })
                .ToList();
            
            var hostConfigFiles = _configFiles
                .Where(x => x.ToLower().Contains("host"))
                .Select(x =>
                {
                    var obj = new DeserializerBuilder()
                        .IgnoreUnmatchedProperties()
                        .Build().Deserialize<HostConfigFileContent>(File.ReadAllText(x));
                    return (path : x, content : obj);
                })
                .ToList();

            var otherFiles = _configFiles
                .Where(x => !(x.ToLower().Contains("host") || x.ToLower().Contains("group")))
                .ToList();

            foreach (var groupConfigFile in groupConfigFiles)
            {
                var hostConfigs = new Dictionary<string, IConfigurationRoot>();
                _groupHostConfig.Add(groupConfigFile.content.GroupName, hostConfigs);
                
                foreach (var hostName in groupConfigFile.content.Hosts)
                {
                    var cb = new ConfigurationBuilder();

                    // add variable file
                    otherFiles.ForEach(x => cb.AddYamlFile(x, false, false));

                   var mountedFiles = _secretFiles.Select(x =>
                    {
                        var encHandler = new EncryptedConfigFileHandler();
                        var path = encHandler.MountPlainFile(x.path, x.passphrase);
                        cb.AddYamlFile(path, false, false);
                        return encHandler;
                    }).ToList();
                    
                    // add group file
                    cb.AddYamlFile(groupConfigFile.path, false, false);

                    // override with host files
                    var hostFile = hostConfigFiles.FirstOrDefault(x => x.content.Name == hostName);
                    cb.AddYamlFile(hostFile.path, false, false);
                    hostConfigs.Add(hostName, cb.Build());
                    mountedFiles.ForEach(x => x.DisposePlainFile());
                }
            }

            foreach (var hostConfigFile in hostConfigFiles)
            {
                var cb = new ConfigurationBuilder();

                // add variable file
                otherFiles.ForEach(x => cb.AddYamlFile(x, false, false));

                var mountedFiles = _secretFiles.Select(x =>
                {
                    var encHandler = new EncryptedConfigFileHandler();
                    var path = encHandler.MountPlainFile(x.path, x.passphrase);
                    cb.AddYamlFile(path, false, false);
                    return encHandler;
                }).ToList();
                
                // add hostfile file
                cb.AddYamlFile(hostConfigFile.path, false, false);

                _hostConfig.Add(hostConfigFile.content.Name, cb.Build());
                mountedFiles.ForEach(x => x.DisposePlainFile());
            }
            
            return new BasicConfig(_groupHostConfig, _hostConfig);
        }
    }
}