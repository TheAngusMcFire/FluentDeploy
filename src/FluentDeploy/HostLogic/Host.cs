using System;
using System.Collections.Generic;
using FluentDeploy.Config;
using FluentDeploy.Execution;

namespace FluentDeploy.HostLogic
{
    public class Host
    {
        public HostConfig Config { get; }
        public HostContext Context { get; } 

        public Host(HostConfig config)
        {
            Context = new HostContext();
            Config = config;
        }

        public Host AddPlaybook(Action<HostContext, HostConfig> playBook)
        {
            playBook(Context, Config);
            return this;
        }
        
        public Host AddPlaybook(Action<HostContext> playBook)
        {
            playBook(Context);
            return this;
        }
    }
}