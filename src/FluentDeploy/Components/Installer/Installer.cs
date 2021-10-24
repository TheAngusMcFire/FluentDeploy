namespace FluentDeploy.Components.Installer
{
    public static class Installer
    {
        public static DockerInstallerBuilder InstallDocker(bool withCompose = false) =>
            new DockerInstallerBuilder(withCompose);
        
        public static QemuLibVirtInstallerBuilder InstallQemuAndLibVirt() =>
            new ();
    }
}