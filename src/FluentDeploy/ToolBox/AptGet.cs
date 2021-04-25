using System.Collections.Generic;
using System.Linq;
using FluentDeploy.Commands;
using FluentDeploy.Execution;

namespace FluentDeploy.ToolBox
{
    public class AptGet : BaseCommandBuilder<AptGet>
    {
        private List<string> _packages;

        private static string PackageList(IEnumerable<string> lst) => lst.Aggregate((s, s1) => $"{s} {s1}");
        
        public static AptGet Install(params string[] packages)
        {
            return new () {_packages =  new List<string>(packages), Name = $"Install packages: {PackageList(packages)}"};
        }

        protected override List<BaseCommand> BuildCommands(IHostInfo hostInfo)
        {
            var lst = new List<BaseCommand>();

            if (!hostInfo.PackageManagerMirrorsUpdated)
            {
                lst.Add(ConsoleCommand.Exec("apt-get")
                    .WithArguments("update"));
            }

            lst.Add(ConsoleCommand.Exec("apt-get")
                .WithArguments("install", "-y", PackageList(_packages)));

            return lst;
        }
    }
}