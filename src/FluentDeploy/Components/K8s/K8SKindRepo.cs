namespace FluentDeploy.Components.K8s
{
    public static class K8SKindRepo
    {
        public const string PersistentVolume = nameof(PersistentVolume);
        public const string PersistentVolumeClaim = nameof(PersistentVolumeClaim);
        public const string Service = nameof(Service);
        public const string Deployment = nameof(Deployment);
        public const string Ingress = nameof(Ingress);
    }
}