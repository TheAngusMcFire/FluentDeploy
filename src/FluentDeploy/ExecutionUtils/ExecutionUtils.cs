using System;
using FluentDeploy.Commands;
using FluentDeploy.ExecutionEngine.Interfaces;
using FluentDeploy.Extentions;

namespace FluentDeploy.ExecutionUtils
{
    public static class ExecutionUtils
    {   
        public static string ExecuteSimpleCommand(ICommandExecutor commandExecutor, string command, string[] args) => 
            commandExecutor.ExecuteConsoleCommand(ConsoleCommand.Exec(command).WithArguments(args))
                .StdOutText.Trim();

        public static int ExecuteSimpleCommandAndGetInt(ICommandExecutor commandExecutor, string command, string[] args) =>
            Convert.ToInt32(ExecuteSimpleCommand(commandExecutor, command, args));
    }
}