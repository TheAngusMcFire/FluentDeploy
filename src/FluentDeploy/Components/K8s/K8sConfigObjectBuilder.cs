using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace FluentDeploy.Components.K8s
{
    public class K8SConfigObjectBuilder
    {
        internal class MetadataObject
        {
            [YamlMember(Alias = "name")]
            public string Name { get; set; }
            [YamlMember(Alias = "namespace")]
            public string NameSpace { get; set; }
            [YamlMember(Alias = "annotations")]
            public Dictionary<string, string> Annotations { get; set; }
        }
        
        public object BuildMetadataObject(string name, string namesp, Dictionary<string, string> annotations)
        {
            return new MetadataObject()
            {
                Name = name,
                NameSpace = namesp,
                Annotations = annotations
            };
        }
        
        public string BuildConfigObject(string kind, string version, object spec, object metadata)
        {
            var obj = new
            {
                apiVersion = version,
                kind = kind,
                metadata = metadata,
                spec = spec
            };
            return new YamlDotNet.Serialization.Serializer().Serialize(obj);
        }
    }
}