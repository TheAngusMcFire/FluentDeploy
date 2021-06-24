using System;
using System.Collections.Generic;
using System.IO;
using FluentDeploy;
using FluentDeploy.Commands;
using FluentDeploy.Commands.Validation;
using FluentDeploy.Components;
using FluentDeploy.Components.Docker;
using FluentDeploy.Config;
using FluentDeploy.Execution;
using FluentDeploy.Extentions;
using FluentDeploy.HostLogic;
using FluentDeploy.ToolBox;
using YamlDotNet.Serialization;

namespace FluentDeployExample
{
    class Program
    {

        public class Config
        {
        }

        public static void TestPlayBook(HostContext context, Config cfg)
        {
            ExecutionUnit.WithName("Copy build artefacts")
                .AddCommand(ConsoleCommand.Exec("cargo").WithArguments("build"))
                .AddCommand(ConsoleCommand.Exec("cp").WithArguments("./loggir/target/debug/loggir", "loggir_exe"))
                .SaveTo(context);

            ExecutionUnit.WithName("Cleanup build artefacts")
                .AddCommand(ConsoleCommand.Exec("rm").WithArguments("-rf",  "loggir_exe"))
                .SaveTo(context);

            context.AsRoot();

            AptGet.Install("wireguard")
                .RunAsRoot()
                .SaveTo(context);
            
            
            

        }


        static void Main(string[] args)
        {
            var config = ConfigLoader.Load("hosts.yaml");
            
            //var deserializer = new Deserializer();
            //var yamlObject = deserializer.Deserialize<Dictionary<string, List<HostConfig>>>(File.ReadAllText("hosts.yaml"));
            
            
            KeyStore.InitKeyStore();
            //var rem = new RemoteExecutor(new HostConfig() { Port = 22, Host = "192.168.1.106", User = "angus" });
            
            var em = new ExecutionManager();

            var host = new Host(null);
            host.AddPlaybook(TestPlayBook, new Config());
            host.AddPlaybook(context => TestPlayBook(context, new Config()));
            

            //em.Local.AddCommand(ConsoleCommand.AsUser("cargo").WithArgument("build"));
            //em.Local.AddCommand(ConsoleCommand.AsUser("cp").WithArguments(new [] {"./loggir/target/debug/loggir",  "loggir_exe"}));
            
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