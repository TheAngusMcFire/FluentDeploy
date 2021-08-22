using FluentDeploy.Commands;
using FluentDeploy.Components.FileSystem;
using FluentDeploy.Components.PackageManagers;
using FluentDeploy.Config;
using FluentDeploy.Enums;
using FluentDeploy.ExecutionUtils;
using FluentDeploy.Extentions;
using FluentDeploy.HostLogic;
using FluentDeploy.Logging;



namespace FluentDeployExample
{
    class Program
    {   
        public static void AptInstallPlayBook(HostContext context, HostConfig cfg)
        {
            context.AsRoot();
            
            AptGet.Upgrade()
                .RunAsRoot()
                .ExecuteOn(context);
            
            AptGet.Install("vim")
                .RunAsRoot()
                .ExecuteOn(context);
        }

        private static void TestPlayBook(HostContext context, HostConfig cfg)
        {
            FileSystem.Directory(context, "/test")
                .Description("Create Test directory")
                .Owner(cfg.HostInfo.User)
                .Group(cfg.HostInfo.User)
                .Permissions(644)
                .ExecuteOn(context);
        }

        static void Main(string[] args)
        {
            KeyStore.InitKeyStore();
            Logging.UseSerilogConsole(false);
            var config = ConfigLoader.Load("hosts.yaml");
            var hostConfig = config.GetUngroupedHostConfig("home_server");
            var host = new Host(hostConfig);

            host.ExecutePlaybook(AptInstallPlayBook);
            host.ExecutePlaybook(TestPlayBook);
        }
    }
}