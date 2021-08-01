using System;
using System.Collections.Generic;
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
            var executor = new RemoteExecutor(config.HostInfo);
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