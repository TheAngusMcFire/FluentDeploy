using System.Collections.Generic;

namespace FluentDeploy.Components.K8s
{
    public class K8SIngressConfigBuilder
    {
        private string _ingressClassName = null;
        private List<object> _tls = null;
        private List<object> _rules = null;
        private Dictionary<string, string> _annotations = null;

        public K8SIngressConfigBuilder(string ingressClassName = null)
        {
            _ingressClassName = ingressClassName;
        }

        public K8SIngressConfigBuilder AddCertManager(string issuer, bool editInPlace = false)
        {
            _annotations ??= new Dictionary<string, string>();
            _annotations.Add("cert-manager.io/cluster-issuer", issuer);
            
            if(editInPlace)
                _annotations.Add("acme.cert-manager.io/http01-edit-in-place", "true");
                
            return this;
        }
        
        public K8SIngressConfigBuilder AddTls(string secretName, params string[] hosts)
        {
            _tls ??= new List<object>();
            _tls.Add(new
            {
                hosts = hosts,
                secretName = secretName
            });
            return this;
        }

        public K8SIngressConfigBuilder AddRule(string host, string serviceName, int port, string path = "/",
            string pathType = "Prefix")
        {
            _rules ??= new List<object>();
            _rules.Add(new
            {
                host = host,
                http = new
                {
                    paths = new object[]
                    {
                        new
                        {
                            path = path,
                            pathType = pathType,
                            backend = new
                            {
                                service = new
                                {
                                    name = serviceName,
                                    port = new
                                    {
                                        number = port
                                    }
                                }
                            }
                        }
                    }
                }
            });
            return this;
        }

        public Dictionary<string, string> GetAnnotations() => _annotations;

        public object Build()
        {
            return new
            {
                ingressClassName = _ingressClassName,
                tls = _tls,
                rules = _rules
            };
        }
    }
}