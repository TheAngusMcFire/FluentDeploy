using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using FluentDeploy;
using FluentDeploy.Commands;
using FluentDeploy.Commands.Validation;
using FluentDeploy.Components;
using FluentDeploy.Components.Docker;
using FluentDeploy.Config;
using FluentDeploy.ExecutionEngine;
using FluentDeploy.ExecutionUtils;
using FluentDeploy.Extentions;
using FluentDeploy.HostLogic;
using FluentDeploy.Logging;
using FluentDeploy.ToolBox;
using YamlDotNet.Serialization;

namespace FluentDeployExample
{
    class Program
    {
        public static void TestPlayBook(HostContext context, HostConfig cfg)
        {
            //ExecutionUnit.WithName("Copy build artefacts")
            //    .AddCommand(ConsoleCommand.Exec("cargo").WithArguments("build"))
            //    .AddCommand(ConsoleCommand.Exec("cp").WithArguments("./loggir/target/debug/loggir", "loggir_exe"))
            //    .SaveTo(context);
//
            //ExecutionUnit.WithName("Cleanup build artefacts")
            //    .AddCommand(ConsoleCommand.Exec("rm").WithArguments("-rf",  "loggir_exe"))
            //    .SaveTo(context);
//
            //context.AsRoot();
//
            //AptGet.Install("wireguard")
            //    .RunAsRoot()
            //    .SaveTo(context);
        }
        
        
        public static void AptInstallPlayBook(HostContext context, HostConfig cfg)
        {
            context.AsRoot();
            
            AptGet.Upgrade()
                .ExecuteOn(context);
            
            AptGet.Install("vim")
                .ExecuteOn(context);
        }

        static void Main(string[] args)
        {
            KeyStore.InitKeyStore();
            Logging.UseSerilogConsole(true);
            var config = ConfigLoader.Load("hosts.yaml");
            var hostConfig = config.GetUngroupedHostConfig("home_server");
            var host = new Host(hostConfig);
            host.ExecutePlaybook(AptInstallPlayBook);

            //engine.ExecuteHost();

            //var hosts = config.GetUngroupedHosts()
            //    .Select(x => new Host(x)
            //        .AddPlaybook(TestPlayBook)
            //        .AddPlaybook(TestPlayBook)
            //        .AddPlaybook(TestPlayBook))
            //    .ToList();
            //    

            //var rem = new RemoteExecutor(new HostConfig() { Port = 22, Host = "192.168.1.106", User = "angus" });

            //var em = new ExecutionManager();
//
            //var host = new Host(null);
            //host.AddPlaybook(TestPlayBook);


            //DockerBuilder.Build(".", "loggir-image-tmp", "./docker/dockerfile_arch")
            //    .Tag("loggir-image")
            //    .Push("docker.rieg.tk")
            //    .GetCompleteImageUrl(out var imageName)
            //    .BuildCommands(em.Local);

            //em.Local.AddCommand(ConsoleCommand.AsUser("rm").WithArguments( new [] {"-rf",  "loggir_exe"}));

            //DockerBuilder.RunDetached(imageName, "loggir_service")
            //    .StopAndDeleteIfRunning()
            //    .AddNetwork("loggir-network")
            //    .AddEnvironmentVariable("SOMETHING", "some_value")
            //    .AddFileMapping(Directory.GetCurrentDirectory(), "/app")
            //    .AddPortForwarding(8081, 8080)
            //    .BuildCommands(em.Remote);

            //rem.ExecuteCommands(new List<BaseCommand>() { ConsoleCommand.AsRoot("ls").WithArguments( new [] {"/bin" }) });

            //executor.Execute();
        }
    }
}