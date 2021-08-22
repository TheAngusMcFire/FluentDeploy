using System;
using System.Collections.Generic;
using System.Linq;
using FluentDeploy.Config;
using FluentDeploy.ExecutionEngine;
using FluentDeploy.ExecutionUtils;

namespace FluentDeploy.HostLogic
{
    public class Host
    {
        public HostConfig Config { get; }
        public HostContext Context { get; } 

        public Host(HostConfig config)
        {
            var hostComps = config.HostInfo.Host.Split(new[] {":"}, StringSplitOptions.RemoveEmptyEntries);
            
            var hostName = hostComps.First();
            var port = hostComps.Select((x, i) => (x, i))
                .Where(x => x.i == 1)
                .Select(x => Convert.ToInt32(x.x))
                .DefaultIfEmpty(22)
                .First();
            
            var executor = new RemoteExecutor(hostName, port, config.HostInfo.User, KeyStore.Default.PrivateKeyFiles);
            var engine = new ExecutionEngine.ExecutionEngine(this, executor);
            Context = new HostContext(engine);
            Config = config;
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