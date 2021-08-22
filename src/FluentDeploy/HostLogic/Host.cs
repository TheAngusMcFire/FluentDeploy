using System;
using System.Linq;
using FluentDeploy.Commands;
using FluentDeploy.Config;
using FluentDeploy.DistributionVariants;
using FluentDeploy.ExecutionEngine;
using FluentDeploy.ExecutionEngine.Interfaces;
using FluentDeploy.ExecutionUtils;
using FluentDeploy.Extentions;
using Serilog;

namespace FluentDeploy.HostLogic
{
    public class Host
    {
        public HostConfig Config { get; }
        public HostContext Context { get; }
        private readonly ILogger _logger;

        private IDistributionVariant ResolveDistributionVariant(string distributionName)
        {
            return distributionName switch
            {
                "Debian" => new DebianVariant(),
                "Arch" => new ArchVariant(),
                _ => throw new ArgumentOutOfRangeException(nameof(distributionName), distributionName, null)
            };
        }

        public Host(HostConfig config)
        {
            _logger = Log.ForContext<Host>();
            var hostComps = config.HostInfo.Host.Split(new[] {":"}, StringSplitOptions.RemoveEmptyEntries);
            
            var hostName = hostComps.First();
            var port = hostComps.Select((x, i) => (x, i))
                .Where(x => x.i == 1)
                .Select(x => Convert.ToInt32(x.x))
                .DefaultIfEmpty(22)
                .First();
            
            var executor = new RemoteExecutor(hostName, port, config.HostInfo.User, KeyStore.Default.PrivateKeyFiles);
            var engine = new ExecutionEngine.ExecutionEngine(this, executor);
            Config = config;
            Context = CreateHostContext(engine, config.HostInfo.Distribution);
        }

        private string ExecuteSimpleCommand(ICommandExecutor commandExecutor, string command, string[] args) => 
            commandExecutor.ExecuteConsoleCommand(ConsoleCommand.Exec(command).WithArguments(args))
            .StdOutText.Trim();
        
        
        private HostContext CreateHostContext(ICommandExecutor commandExecutor, string configDistroName)
        {
            var distributionName = configDistroName ??=
                ExecuteSimpleCommand(commandExecutor, "lsb_release", new[] {"-s", "-i"});

            var distributionVariant = ResolveDistributionVariant(distributionName);
            
            var userId = ExecuteSimpleCommand(commandExecutor, "id", new[] {"-u", Config.HostInfo.User}); 
            var userGroupId = ExecuteSimpleCommand(commandExecutor, "id", new[] {"-g", Config.HostInfo.User});

           return new HostContext(commandExecutor)
           {
               PackageManagerMirrorsUpdated = false,
               UserId = Convert.ToInt32(userId),
               UserGroupId = Convert.ToInt32(userGroupId),
               DistributionVariant = distributionVariant
           };
        }

        public Host ExecutePlaybook(Action<HostContext, HostConfig> playBook)
        {
            playBook(Context, Config);
            return this;
        }
        
        public Host ExecutePlaybook(Action<HostContext> playBook)
        {
            playBook(Context);
            return this;
        }
    }
}