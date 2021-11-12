using System;
using System.Collections.Generic;
using System.Linq;
using FluentDeploy.Config;
using FluentDeploy.HostLogic;
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
            return AddPlaybook($"{typeName}.{methodName}", playBook);
        }

        public PlaybookRegistry AddPlaybook(string name, Action<HostContext, HostConfig> playBook)
        {
            _playbooks.Add(name, playBook);
            return this;
        }
        
        private void PrintUsage()
        {
            Log.Information("Usage:");
            Log.Information("    list    | List the available playbooks and hosts");
            Log.Information("    <host_name> \"<Playbook>,<Playbook>,<Playbook>,...\"    | execute playbooks on hosts, can also be playbook indices");
            Environment.Exit(-1);
        }

        private string GetSep()
        {
            var sepLen = Console.WindowWidth - 20;
            Log.Information("{0}",sepLen);
            var sep = "";
            while (sepLen-- > 0) sep += "-";
            return sep;
        }
        
        private void ListHostsAndPlaybooks()
        {
            var mayIndex = Math.Max(_playbooks.Count, _hostsAndGroup.Length);
            var playbookNames = _playbooks.Select(x => x.Key).ToArray();
            var spacing = -(_hostsAndGroup.Max(x => x.Length) + 2);

            Log.Information(GetSep());
            Log.Information($"{{0,{spacing.ToString()}}} |    {{1}}", "Hosts:", "Playbooks:");
            Log.Information(GetSep());
            for (var index = 0; index < mayIndex; index++)
            {
                var playbook = index < playbookNames.Length ? playbookNames[index] : ""; 
                Log.Information($"{{0,{spacing.ToString()}}} | {{1,-2}} {{2}} ", index < _hostsAndGroup.Length ? _hostsAndGroup[index] : "", index, playbook);
            }
            
            Environment.Exit(0);
        }

        private void ExecutePlaybooks(string hostName, string playbooks, BasicConfig config)
        {
            var hostConfig = config.GetHostConfig(hostName);
            var host = Host.BuildHost(hostConfig, config);

            var playbookNames = _playbooks.Select(x => x.Key).ToArray();
            var comps = playbooks.Split(new[] {","}, StringSplitOptions.RemoveEmptyEntries);
            
            foreach (var playBook in comps)
            {
                var targetBook = int.TryParse(playBook, out int idx) ? playbookNames[idx] : playBook;
                host.ExecutePlaybook(_playbooks[targetBook]);
            }

            Environment.Exit(0);
        }

        public void DispatchDefaultOptions(string[] args, BasicConfig config)
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
                ExecutePlaybooks(args[0], args[1], config);
            }
            
            PrintUsage();
        }
    }
}