using System;
using System.IO;
using FluentDeploy;
using FluentDeploy.Commands;
using FluentDeploy.Commands.Validation;
using FluentDeploy.Components;
using FluentDeploy.Components.Docker;

namespace FluentDeployExample
{
    class Program
    {
        static void Main(string[] args)
        {
            var em = new ExecutionManager();
            
            em.Local.AddCommand(ConsoleCommand.AsUser("cargo").WithArgument("build"));
            em.Local.AddCommand(ConsoleCommand.AsUser("cp").WithArguments(new [] {"./loggir/target/debug/loggir",  "loggir_exe"}));
            
            DockerBuilder.Build(".", "loggir-image-tmp", "./docker/dockerfile_arch")
                .Tag("loggir-image")
                .Push("docker.rieg.tk")
                .GetCompleteImageUrl(out var imageName)
                .BuildCommands(em.Local);
            
            em.Local.AddCommand(ConsoleCommand.AsUser("rm").WithArguments( new [] {"-rf",  "loggir_exe"}));
            
            DockerBuilder.RunDetached(imageName, "loggir_service")
                .StopAndDeleteIfRunning()
                .AddNetwork("loggir-network")
                .AddEnvironmentVariable("SOMETHING", "some_value")
                .AddFileMapping(Directory.GetCurrentDirectory(), "/app")
                .AddPortForwarding(8081, 8080)
                .BuildCommands(em.Remote);
            
            //executor.Execute();
        }
    }
}