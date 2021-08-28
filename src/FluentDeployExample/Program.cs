using System;
using System.Linq;
using System.Net.Http;
using FluentDeploy.Commands;
using FluentDeploy.Components.Curl;
using FluentDeploy.Components.Docker;
using FluentDeploy.Components.Docker.DockerApi;
using FluentDeploy.Components.FileSystem;
using FluentDeploy.Components.PackageManagers;
using FluentDeploy.Config;
using FluentDeploy.Enums;
using FluentDeploy.ExecutionUtils;
using FluentDeploy.Extentions;
using FluentDeploy.HostLogic;
using FluentDeploy.Logging;
using Serilog;


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
                .Permissions(744)
                .ExecuteOn(context);
            
            FileSystem.File(context, "/test/test.txt")
                .Content("#!/bin/python\nprint('Hello World')")
                .CurrentUserAsOwnerAndGroup()
                .Permissions(744)
                .ExecuteOn(context); 
        }
        
        private static void CurlTestPlayBook(HostContext context, HostConfig cfg)
        {
           var curl = new CurlCommandBuilder("http://localhost/v1.40/networks", HttpMethod.Get, "/var/run/docker.sock")
               .RunAsRoot()
               .ExecuteOn(context);
           Log.Information(curl.Response);
           Log.Information(curl.HttpStatusCode.ToString());
        }
        
        private static void DockerApiTestPlayBook(HostContext context, HostConfig cfg)
        {
            context.AsRoot();
            Docker.Network("TestNetwork")
                .Description("Create docker Network for Test")
                .ExecuteOn(context);
            
            Docker.Container("TestContainer", "debian:latest")
                .AddNetwork("TestNetwork")
                .AddMount("/tmp", "/app")
                .AddPortMapping("22/tcp", "127.0.0.1", 22)
                .AddPortMapping("23/tcp", "0.0.0.0", 23)
                .Commands("echo", "Hello World")
                .Restart()
                .ExecuteOn(context);

            var client = new CurlDockerHttpClient(context, true);
            var api = new DockerApi(client);
            var net = api.InspectContainer("TestContainer");
            //var cre = api.CreateNetwork("TestNetwork");

            //var containers = api.GetContainers(true);
            //var fac = containers.First(x => x.Names.Contains("/factorio_main"));
            //var des = api.InspectContainer(fac.Id);
        }

        static void Main(string[] args)
        {
            KeyStore.InitKeyStore();
            Logging.UseSerilogConsole(true);
            var config = ConfigLoader.Load("hosts.yaml");
            var hostConfig = config.GetUngroupedHostConfig("home_server");
            var host = new Host(hostConfig);

            host.ExecutePlaybook(DockerApiTestPlayBook);
            //host.ExecutePlaybook(AptInstallPlayBook);
            //host.ExecutePlaybook(TestPlayBook);
        }
    }
}