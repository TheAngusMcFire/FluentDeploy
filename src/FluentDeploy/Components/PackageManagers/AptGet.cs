using System.Collections.Generic;
using System.Linq;
using FluentDeploy.Commands;
using FluentDeploy.Commands.Validation;
using FluentDeploy.ExecutionUtils.Interfaces;
using FluentDeploy.Extentions;

namespace FluentDeploy.Components.PackageManagers
{
    public class AptGet : BaseCommandBuilder<AptGet>
    {
        private List<string> _packages;

        private string _targetCommand;
        private bool _update;
        private bool _upgrade;

        private static string PackageList(IEnumerable<string> lst)
        {
            var enumerable = lst as string[] ?? lst.ToArray();
            return enumerable.Any() ? enumerable.Aggregate((s, s1) => $"{s} {s1}") : null;
        }

        public static AptGet Install(params string[] packages)
        {
            return new () {_packages =  new List<string>(packages), Name = $"Install packages: {PackageList(packages)}", _targetCommand = "install"};
        }

        public static AptGet Update()
        {
            return new () {_packages =  new List<string>(), Name = $"Update mirror", _update = true, _upgrade = false};
        }
        
        public static AptGet Upgrade()
        {
            return new () {_packages =  new List<string>(), Name = $"Upgrade Packages", _update = true, _upgrade = true};
        }

        private bool IsPacketInstalled(IExecutionContext context, string packetName)
        {
            return ArePackagesInstalled(context, packetName);
        }

        private bool ArePackagesInstalled(IExecutionContext context, params string[] packetNames)
        {
            var argLst = new List<string>();
            argLst.Add("-s");
            argLst.AddRange(packetNames);
            var result = context
                .ExecuteConsoleCommand(ConsoleCommand.Exec("dpkg")
                .WithValidator(new ConstResultCommandExecutionValidator(CommandExecutionValidationResult.SuccessResult))
                .WithArguments(argLst.ToArray()));

            return result.ReturnCode == 0;
        }

        protected override void Execute(IExecutionContext context)
        {
            if (!context.PackageManagerMirrorsUpdated || _update)
            {
                context.ExecuteCommand(ConsoleCommand.Exec("apt-get")
                    .WithArguments("update"));
                context.ExecuteCommand(CommandStore.PackageManagerUpdated());
            }

            if (_upgrade)
            {
                context.ExecuteCommand(ConsoleCommand.Exec("apt-get")
                    .WithArguments("upgrade"));
            }

            var argsLst = new List<string>();
            argsLst.Add(_targetCommand);
            argsLst.Add("-y");
            argsLst.AddRange(_packages ?? new List<string>());

            if (_packages == null || _packages.Count == 0 || ArePackagesInstalled(context, _packages.ToArray()))
            {
                return;
            }

            context.ExecuteCommand(ConsoleCommand.Exec("apt-get")
                .WithArguments(argsLst.ToArray()));
        }
    }
}