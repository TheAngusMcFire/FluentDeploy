using System;
using System.Collections.Generic;
using System.Linq;
using FluentDeploy.Commands;
using FluentDeploy.Commands.Validation;
using FluentDeploy.ExecutionUtils.Interfaces;
using FluentDeploy.Extentions;

namespace FluentDeploy.ToolBox
{
    public class AptGet : BaseCommandBuilder<AptGet>
    {
        private List<string> _packages;

        private string _targetCommand;
        
        private static string PackageList(IEnumerable<string> lst) => lst.Count() != 0 ? lst.Aggregate((s, s1) => $"{s} {s1}") : null;

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
            if (!context.PackageManagerMirrorsUpdated)
            {
                context.ExecuteCommand(ConsoleCommand.Exec("apt-get")
                    .WithArguments("update"));
                context.ExecuteCommand(CommandStore.PackageManagerUpdated());
            }

            var packages = PackageList(_packages);
            var args = packages == null ? new[] {_targetCommand, "-y"} : new[] {_targetCommand, "-y", packages};

            if (packages == null || ArePackagesInstalled(context, _packages.ToArray()))
            {
                return;
            }

            context.ExecuteCommand(ConsoleCommand.Exec("apt-get")
                .WithArguments(args));
        }
    }
}