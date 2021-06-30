using System;
using System.Collections.Generic;
using System.Linq;
using FluentDeploy.Commands;
using FluentDeploy.Execution;

namespace FluentDeploy.ToolBox
{
    public class AptGet : BaseActiveCommandBuilder<AptGet>
    {
        private List<string> _packages;

        private string _targetCommand;
        
        private static string PackageList(IEnumerable<string> lst) => lst.Count() != 0 ? lst.Aggregate((s, s1) => $"{s} {s1}") : string.Empty;
        
        public static AptGet Install(params string[] packages)
        {
            return new () {_packages =  new List<string>(packages), Name = $"Install packages: {PackageList(packages)}", _targetCommand = "install"};
        }

        public static AptGet Update()
        {
            return new () {_packages =  new List<string>(), Name = $"Update mirror", _targetCommand = "update"};
        }

        public static AptGet Upgrade()
        {
            return new () {_packages =  new List<string>(), Name = $"Upgrade Packages", _targetCommand = "upgrade"};
        }
        
        protected override List<BaseCommand> BuildCommands(IHostInfo hostInfo)
        {
            var lst = new List<BaseCommand>();

            if (!hostInfo.PackageManagerMirrorsUpdated)
            {
                lst.Add(ConsoleCommand.Exec("apt-get")
                    .WithArguments("update"));
                lst.Add(CommandStore.PackageManagerUpdated());
            }

            lst.Add(ConsoleCommand.Exec("apt-get")
                .WithArguments(_targetCommand, "-y", PackageList(_packages)));

            return lst;
        }
    }
}