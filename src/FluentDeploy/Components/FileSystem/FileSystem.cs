

using FluentDeploy.ExecutionUtils.Interfaces;

namespace FluentDeploy.Components.FileSystem
{
    public class FileSystem
    {
        public static DirectoryStateBuilder Directory(IHostInfo info, string path) => 
            new DirectoryStateBuilder(info, path);

        public static DirectoryStateBuilder Directory(IHostInfo info, string path, string owner, string group,
            short permissions) =>
            new DirectoryStateBuilder(info, path)
                .Owner(owner)
                .Group(group)
                .Permissions(permissions);
    }
}