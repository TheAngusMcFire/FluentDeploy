using FluentDeploy.Commands;
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
            context.ExecuteCommand(new FileOperationCommand()
            {
                FileOperationType = FileOperationType.CreateDirectory,
                Destination = "/test"
            });
        }

        static void Main(string[] args)
        {
            KeyStore.InitKeyStore();
            Logging.UseSerilogConsole(true);
            var config = ConfigLoader.Load("hosts.yaml");
            var hostConfig = config.GetUngroupedHostConfig("home_server");
            var host = new Host(hostConfig);

            host.ExecutePlaybook(TestPlayBook);
        }
    }
}