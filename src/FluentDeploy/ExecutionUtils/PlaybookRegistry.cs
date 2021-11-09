using System;
using System.Collections.Generic;
using System.Linq;
using FluentDeploy.Config;
using Serilog;

namespace FluentDeploy.ExecutionUtils
{
    public class PlaybookRegistry
    {
        private Dictionary<string, Action<HostContext, HostConfig>> _playbooks;
        private string[] _hostsAndGroup; 

        public PlaybookRegistry(string[] hostsAndGroup)
        {
            _hostsAndGroup = hostsAndGroup;
            _playbooks = new Dictionary<string, Action<HostContext, HostConfig>>();
        }

        public PlaybookRegistry AddPlaybook(Action<HostContext, HostConfig> playBook)
        {
            var typeName = playBook.Method.ReflectedType?.FullName;

            if (typeName is not null)
                typeName = typeName.Split(new[] {"."}, StringSplitOptions.RemoveEmptyEntries)
                    .Last();

            var methodName = playBook.Method.Name;
            _playbooks.Add($"{typeName}.{methodName}", playBook);
            return this;
        }

        private void PrintUsage()
        {
            Log.Information("Usage:");
            Log.Information("    list    | List the available playbooks and hosts");
            Log.Information("    <host_name> \"<Playbook>,<Playbook>,<Playbook>,...\"    | execute playbooks on hosts, can also be playbook indices");
            Environment.Exit(-1);
        }

        private void ListHostsAndPlaybooks()
        {
            var mayIndex = Math.Max(_playbooks.Count, _hostsAndGroup.Length);
            var playbookNames = _playbooks.Select(x => x.Key).ToArray();
            var spacing = -(_hostsAndGroup.Max(x => x.Length) + 2);

            Log.Information($"--------------------------------------------------------------------------------");
            Log.Information($"{{0,{spacing.ToString()}}} |    {{1}}", "Hosts:", "Playbooks:");
            Log.Information($"--------------------------------------------------------------------------------");
            for (var index = 0; index < mayIndex; index++)
            {
                var playbook = index < playbookNames.Length ? playbookNames[index] : ""; 
                Log.Information($"{{0,{spacing.ToString()}}} | {{1,-2}} {{2}} ", index < _hostsAndGroup.Length ? _hostsAndGroup[index] : "", index, playbook);
            }
            
            Environment.Exit(0);
        }

        private void ExecutePlaybooks(string host, string playbooks)
        {
            Environment.Exit(0);
        }

        public void DispatchDefaultOptions(string[] args)
        {
            if (args.Length == 1)
            {
                if(args[0] == "help")
                    PrintUsage();
                
                if(args[0] == "list")
                    ListHostsAndPlaybooks();
            }
            
            if (args.Length == 2)
            {
                ExecutePlaybooks(args[0], args[1]);
            }
            
            PrintUsage();
        }
    }
}