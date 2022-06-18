using System.Collections.Generic;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace FluentDeploy.Components.K8s
{
    public interface IK8SConfigObjectBuilder
    {
        string BuildConfig();
    }
    
    public class K8SConfigObjectBuilder : IK8SConfigObjectBuilder
    {
        internal class MetadataObject
        {
            public string Name { get; set; }
            public string Namespace { get; set; }
            public Dictionary<string, string> Annotations { get; set; }
        }

        private object baseObject;
        private MetadataObject _metadataObject; 
        
        public K8SConfigObjectBuilder SetConfigObjectMetadata(string name, string namesp, Dictionary<string, string> annotations)
        {
            _metadataObject = new MetadataObject()
            {
                Name = name,
                Namespace = namesp,
                Annotations = annotations
            };
            return this;
        }
        
        public K8SConfigObjectBuilder SetConfigObjectHeader(string kind, string version, object spec)
        {
            baseObject = new
            {
                apiVersion = version,
                kind = kind,
                metadata = _metadataObject,
                spec = spec
            };
            return this;
        }

        public string BuildConfig()
        {
            return new SerializerBuilder()
                .ConfigureDefaultValuesHandling(DefaultValuesHandling.OmitNull)
                .WithNamingConvention(CamelCaseNamingConvention.Instance)
                .DisableAliases()
                .Build()
                .Serialize(this.baseObject);
        }
    }
}