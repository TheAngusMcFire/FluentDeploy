namespace FluentDeploy.Components.Docker
{
    public class Docker
    {
        public static DockerNetworkBuilder Network(string name) => new DockerNetworkBuilder(name);
        public static DockerContainerBuilder Container(string name, string image) => new DockerContainerBuilder(name, image);
    }
}