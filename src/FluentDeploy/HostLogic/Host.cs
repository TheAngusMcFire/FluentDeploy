using System;
using System.Collections.Generic;
using FluentDeploy.Config;
using FluentDeploy.Execution;

namespace FluentDeploy.HostLogic
{
    public class Host
    {
        private readonly HostConfig _config;
        
        
        
        public Host(HostConfig config)
        {
            _config = config;
        }

        public Host AddPlaybook<TCfg1>(Action<HostContext, TCfg1> playBook, TCfg1 config)
        {
            return this;
        }
        
        public Host AddPlaybook<TCfg1, TCfg2>(Action<HostContext, TCfg1> playBook, TCfg1 config1, TCfg2 config2)
        {
            return this;
        }
        
        public Host AddPlaybook(Action<HostContext> playBook)
        {
            return this;
        }


    }
}