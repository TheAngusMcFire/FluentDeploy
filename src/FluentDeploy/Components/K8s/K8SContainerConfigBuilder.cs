using System.Collections.Generic;

namespace FluentDeploy.Components.K8s
{
    public class K8SContainerConfigBuilder
    {
        private string _image = null;
        public string _name = null;
        private List<object> _ports = null;
        private List<object> _volumeMounts = null;
        private List<object> _env = null;

        public K8SContainerConfigBuilder(string name, string image)
        {
            _image = image;
            _name = name;
        }

        public K8SContainerConfigBuilder AddContainerPort(int port)
        {
            _ports ??= new List<object>();
            _ports.Add(new
            {
                containerPort = port
            });
            return this;
        }
        public K8SContainerConfigBuilder AddEnvVar(string key, string value)
        {
            _env ??= new List<object>();
            _env.Add(new
            {
                name = key,
                value = value
            });
            return this;
        }
        
        public K8SContainerConfigBuilder AddVolumeMount(string name, string mountPath, string subPath = null, bool? readOnly = null)
        {
            _volumeMounts ??= new List<object>();
            _volumeMounts.Add(new
            {
                name = name,
                mountPath = mountPath,
                subPath = subPath,
                readOnly = readOnly
            });
            return this;
        }

        public object Build()
        {
            return new
            {
                image = _image,
                name = _name,
                ports = _ports,
                volumeMounts = _volumeMounts,
                env = _env
            };
        }
    }
}