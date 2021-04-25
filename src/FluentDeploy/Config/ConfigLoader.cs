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
        public static BasicConfig Load(string filePath)
        {
            using var input = File.OpenText(filePath); 

            var deserializer = new DeserializerBuilder().Build();
            var parser = new Parser(input);
            parser.Consume<StreamStart>();
            parser.Accept<DocumentStart>(out var _);
            var hosts = deserializer.Deserialize<Dictionary<string, List<HostConfig>>>(parser);
            parser.Accept<DocumentStart>(out var _);
            var vars = deserializer.Deserialize<Dictionary<string, Dictionary<string, string>>>(parser);

            return new BasicConfig() {GroupHosts = hosts, GroupHostVars = vars};
        }
    }
}