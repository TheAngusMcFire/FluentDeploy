namespace FluentDeploy.Components.Docker
{
    public class Docker
    {
        public static DockerNetworkBuilder Network(string name) 
            => new DockerNetworkBuilder(name);
        public static DockerContainerBuilder Container(string name, string image) 
            => new DockerContainerBuilder(name, image);
        
        public static DockerUtilsBuilder StartContainer(string name)
            => new DockerUtilsBuilder(DockerOperation.StartContainer)
                .SetContainerName(name);
        
        public static DockerUtilsBuilder StopContainer(string name)
            => new DockerUtilsBuilder(DockerOperation.StopContainer)
                .SetContainerName(name);
        
        public static DockerUtilsBuilder RemoveContainer(string name)
            => new DockerUtilsBuilder(DockerOperation.RemoveContainer)
                .SetContainerName(name);
        
        public static DockerUtilsBuilder RenameContainer(string name, string newName)
            => new DockerUtilsBuilder(DockerOperation.RenameContainer)
                .SetContainerName(name)
                .SetNewContainerName(newName);
        
        public static DockerUtilsBuilder PruneImages(bool onlyDanglingImages)
            => new DockerUtilsBuilder(DockerOperation.PruneImages)
                .OnlyDanglingImages(onlyDanglingImages);
        
    }
}