using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Serilog;

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
                    .SetConfigObjectHeader(K8SKindRepo.Deployment, K8SVersionRepo.Apps, builder.Build()));
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
                    .SetConfigObjectHeader(K8SKindRepo.Ingress, K8SVersionRepo.Networking, spec));
            return this;
        }

        public K8SConfigBuilder AddConfigMap(out string newName, string name = null, Dictionary<string, string> data = null)
        {
            newName = name ?? $"{_baseName}-cm";
            _builders.Add(
                new K8SConfigObjectBuilder().SetConfigObjectMetadata(newName, Namespace, null)
                    .SetConfigObjectHeaderForConfigMap(K8SKindRepo.ConfigMap, K8SVersionRepo.Default, data));
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
            Log.Debug(builder.ToString());
            return builder.ToString();
        }

        public string GetAndDumpConfigFile()
        {
            var configFile = GetConfigFile();
            Log.Information(configFile);
            return configFile;
        }
        
        public void ApplyTo(string configPath)
        {
            var configFile = GetConfigFile();
            var process = new Process();
            var nfo = new ProcessStartInfo("kubectl");
            nfo.Arguments = $"--kubeconfig {configPath} apply -f -";
            nfo.RedirectStandardInput = true;
            nfo.RedirectStandardOutput = true;
            nfo.RedirectStandardError = true;
            nfo.UseShellExecute = false;
            process.StartInfo = nfo;
            process.Start();
            
            process.StandardInput.Write(configFile);
            process.StandardInput.Flush();
            process.StandardInput.Close();
            process.WaitForExit();
            var error = process.StandardError.ReadToEnd();
            var stdout = process.StandardOutput.ReadToEnd();
            if (error.Length is not 0)
            {
                Log.Information(configFile);
                Log.Information(stdout);
                Log.Error("Error applying configuration {0}", error);
            }
        }
    }
}