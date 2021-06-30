using System.Collections.Generic;
using System.IO;
using System.Reflection.Metadata;
using YamlDotNet.Core;
using YamlDotNet.Core.Events;
using YamlDotNet.Serialization;

namespace FluentDeploy.Config
{
    public class ConfigLoader
    {
        private List<HostInformation> _hostConfigs;

        public ConfigLoader(List<HostInformation> hostConfigs)
        {
            _hostConfigs = hostConfigs;
        }

        public void LoadHostConfig(string fileName)
        {
            using var input = File.OpenText(fileName); 

            var deserializer = new DeserializerBuilder().Build();
            var parser = new Parser(input);
            parser.Consume<StreamStart>();
            parser.Accept<DocumentStart>(out var _);
            var hosts = deserializer.Deserialize<Dictionary<string, List<HostInformation>>>(parser);
            parser.Accept<DocumentStart>(out var _);
            var vars = deserializer.Deserialize<Dictionary<string, Dictionary<string, string>>>(parser);
        }

        public void LoadHostConfigs(params string[] fileNames)
        {
            
        }

        public void LoadVariableFiles(params string[] fileNames)
        {
            
        }

        public BasicConfig GetFinishedConfig()
        {
            return null;
        }


        public static BasicConfig Load(string filePath)
        {
            using var input = File.OpenText(filePath); 

            var deserializer = new DeserializerBuilder().Build();
            var parser = new Parser(input);
            parser.Consume<StreamStart>();
            parser.Accept<DocumentStart>(out var _);
            var hosts = deserializer.Deserialize<Dictionary<string, List<HostInformation>>>(parser);
            parser.Accept<DocumentStart>(out var _);
            var vars = deserializer.Deserialize<Dictionary<string, Dictionary<string, string>>>(parser);

            return new BasicConfig() {GroupHosts = hosts, GroupHostVars = vars};
        }
    }
}