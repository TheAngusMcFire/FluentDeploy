namespace FluentDeploy.Components.K8s
{
    public class K8SServiceType
    {
        public const string LoadBalancer = nameof(LoadBalancer);
        public const string ClusterIP = nameof(ClusterIP);
        public const string ExternalName = nameof(ExternalName);
        public const string NodePort = nameof(NodePort);
    }
}