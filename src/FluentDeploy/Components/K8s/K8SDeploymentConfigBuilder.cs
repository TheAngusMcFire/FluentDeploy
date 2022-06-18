using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;

namespace FluentDeploy.Components.K8s
{
    public class K8SDeploymentConfigBuilder
    {
        private string _baseName = null;
        private int _replicas = 1;
        private List<object> _volumes = null;
        private List<object> _containers = null;

        public K8SDeploymentConfigBuilder(string baseName)
        {
            _baseName = baseName;
        }
        
        public K8SDeploymentConfigBuilder Replicas(int replicas)
        {
            _replicas = replicas;
            return this;
        }

        public K8SDeploymentConfigBuilder AddContainer(string name, string image, Action<K8SContainerConfigBuilder> builderFn = null)
        {
            var builder = new K8SContainerConfigBuilder(name, image);
            if (builderFn is not null)
                builderFn(builder);
            _containers ??= new List<object>();
            _containers.Add(builder.Build());
            return this;
        }
        
        public K8SDeploymentConfigBuilder AddPersistantVolumeClaim(string name, string claimName)
        {
            _volumes ??= new List<object>();
            _volumes.Add(new
            {
                name = name,
                persistentVolumeClaim = new
                {
                    claimName = claimName
                }
            });
            return this;
        }
        
        public K8SDeploymentConfigBuilder AddPersistantVolumeConfigMap(string name, string configMapName, params (string key, string path)[] itemArgs)
        {
            _volumes ??= new List<object>();
            object[] items = null;
            if (itemArgs.Length is not 0)
                items = itemArgs.Select(x => new {key = x.key, path = x.path}).ToArray();
            _volumes.Add(new
            {
                name = name,
                configMap = new
                {
                    name = configMapName,
                    items = items
                }
            });
            return this;
        }
        
        public object Build()
        {
            return new
            {
                replicas = _replicas,
                selector = new
                {
                    matchLabels = new
                    {
                        app = _baseName
                    }
                },
                template = new
                {
                    meatdata = new
                    {
                        labels = new
                        {
                            app = _baseName
                        }
                    },
                    spec = new
                    {
                        volumes = _volumes,
                        containers = _containers
                    }
                }
            };
        }
        
    }
}