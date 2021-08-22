using System;
using FluentDeploy.ExecutionEngine.Interfaces;


namespace FluentDeploy.Components.FileSystem
{
    public static class FileSystemExtensions
    {
        public static int GetUserIdOfUser(this ICommandExecutor commandExecutor, string userName) =>
            ExecutionUtils.ExecutionUtils.ExecuteSimpleCommandAndGetInt(commandExecutor, "id", new[] {"-u", userName});
        public static int GetGroupIdOfUser(this ICommandExecutor commandExecutor, string userName) =>
            ExecutionUtils.ExecutionUtils.ExecuteSimpleCommandAndGetInt(commandExecutor, "id", new[] {"-g", userName});
    }
}