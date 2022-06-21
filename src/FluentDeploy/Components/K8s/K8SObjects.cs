namespace FluentDeploy.Components.K8s
{
    public class K8SProtocol
    {
        public const string TCP = nameof(TCP);
        public const string UDP = nameof(UDP);
    }
    public class K8SPort
    {
        public string Protocol { get; set; }
        public int Port { get; set; }
        public int? TargetPort { get; set; }
    }
}