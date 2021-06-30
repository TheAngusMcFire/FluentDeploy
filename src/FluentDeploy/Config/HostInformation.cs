namespace FluentDeploy.Config
{
    public class HostInformation
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Host { get; set; }
        // todo move the host validation logic
        public int Port { get; set; }
        public string User { get; set; }
    }
}