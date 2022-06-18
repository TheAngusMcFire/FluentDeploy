using System;
using System.Collections.Generic;
using System.Text;

namespace FluentDeploy.Components.K8s
{
    public class K8SConfigBuilder
    {
        private readonly List<IK8SConfigObjectBuilder> _builders = new List<IK8SConfigObjectBuilder>();
        private string Namespace { get; set; }
        private string _baseName;

        public K8SConfigBuilder(string baseName)
        {
            _baseName = baseName;
        }
        
        public K8SConfigBuilder InNameSpace(string ns, bool create = true)
        {
            Namespace = ns;
            _builders.Add(
                new K8SConfigObjectBuilder().SetConfigObjectMetadata(ns, null, null)
                .SetConfigObjectHeader(K8SKindRepo.Namespace, K8SVersionRepo.Default, null));
            
            return this;
        }

        public K8SConfigBuilder AddNfsPersistentVolume(out string newName, string name = null, string path = null, string server = null, string storageClass = null, string capacity = null)
        {
            newName = name ?? $"{_baseName}-pv";
            _builders.Add(
                new K8SConfigObjectBuilder().SetConfigObjectMetadata(newName, Namespace, null)
                    .SetConfigObjectHeader(K8SKindRepo.PersistentVolume, K8SVersionRepo.Default, new
                    {
                        storageClassName = storageClass,
                        capacity = new {
                            storage = capacity
                        },
                        accessModes = new [] {"ReadWriteMany"},
                        nfs = new
                        {
                            path = path,
                            server = server
                        }
                    }));
            return this;
        }
        
        public K8SConfigBuilder AddNfsPersistentVolumeClaim(out string newName, string name = null, string storageClass = null, string capacity = null, string volumeName = null)
        {
            newName = name ?? $"{_baseName}-pvc";
            _builders.Add(
                new K8SConfigObjectBuilder().SetConfigObjectMetadata(newName, Namespace, null)
                    .SetConfigObjectHeader(K8SKindRepo.PersistentVolumeClaim, K8SVersionRepo.Default, new
                    {
                        storageClassName = storageClass,
                        volumeName = volumeName,
                        resources = new {
                            requests =  new {
                                storage = capacity
                            },
                        },
                        accessModes = new [] {"ReadWriteMany"},
                    }));
            return this;
        }
        
        public K8SConfigBuilder AddService(out string newName, string name = null, string selector = null, string type = null, params K8SPort[] ports)
        {
            newName = name ?? $"{_baseName}-svc";
            _builders.Add(
                new K8SConfigObjectBuilder().SetConfigObjectMetadata(newName, Namespace, null)
                    .SetConfigObjectHeader(K8SKindRepo.Service, K8SVersionRepo.Default, new
                    {
                        type = type,
                        selector = new {
                            app = selector
                        },
                        ports = ports
                    }));
            return this;
        }

        public K8SConfigBuilder AddDeployment(out string newName, string name = null, Action<K8SDeploymentConfigBuilder> builderFn = null)
        {
            newName = name ?? $"{_baseName}-depl";
            var builder = new K8SDeploymentConfigBuilder(name ?? _baseName);
            if(builderFn is not null)
                builderFn(builder);
            _builders.Add(
                new K8SConfigObjectBuilder().SetConfigObjectMetadata(newName, Namespace, null)
                    .SetConfigObjectHeader(K8SKindRepo.Deployment, K8SVersionRepo.Default, builder.Build()));
            return this;
        }
        
        public K8SConfigBuilder AddIngress(out string newName, string name = null, string ingressClass = null, Action<K8SIngressConfigBuilder> builderFn = null)
        {
            newName = name ?? $"{_baseName}-ingress";
            var builder = new K8SIngressConfigBuilder(ingressClass);
            if (builderFn != null) builderFn(builder);
            var spec = builder.Build();
            var annotations = builder.GetAnnotations();
            _builders.Add(
                new K8SConfigObjectBuilder().SetConfigObjectMetadata(newName, Namespace, annotations)
                    .SetConfigObjectHeader(K8SKindRepo.Ingress, K8SVersionRepo.Default, spec));
            return this;
        }

        public string GetConfigFile()
        {
            var builder = new StringBuilder();
            this._builders.ForEach(x =>
            {
                builder.AppendLine("---");
                builder.AppendLine(x.BuildConfig());
            });
            Console.WriteLine(builder.ToString());
            return builder.ToString();
        }
    }
}