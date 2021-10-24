

using System;
using FluentDeploy.Commands;
using FluentDeploy.Components.Etc;
using FluentDeploy.Enums;
using FluentDeploy.ExecutionEngine.ExecutionResults;
using FluentDeploy.ExecutionUtils.Interfaces;

namespace FluentDeploy.Components.FileSystem
{
    public class FileSystem
    {
        public static DirectoryStateBuilder Directory(IHostInfo info, string path) => 
            new DirectoryStateBuilder(info, path);

        public static void Directories(IHostInfo info, Action<DirectoryStateBuilder> buildFunc, params string[] paths)
        {
            foreach (var path in paths)
            {
                var builder = new DirectoryStateBuilder(info, path);
                buildFunc(builder);
            }
        }

        public static DirectoryStateBuilder Directory(IHostInfo info, string path, string owner, string group,
            short permissions) =>
            new DirectoryStateBuilder(info, path)
                .Owner(owner)
                .Group(group)
                .Permissions(permissions);

        public static FileStateBuilder File(IHostInfo info, string path) => 
            new FileStateBuilder(info, path);

        public static DirectExecutionBuilder Delete(string path) =>
            new(new FileOperationCommand() {FileOperationType = FileOperationType.Delete, Path = path});

        public static bool Exists(string path, IExecutionContext context)
        {
            var result = context.ExecuteCommand(new FileOperationCommand()
                {FileOperationType = FileOperationType.Exists, Path = path}) as FileOperationExecutionResult;

            return result != null && result.Exists;
        }
        
        public static void SymbolicLink(string source, string destination, IExecutionContext context) => 
            context.ExecuteCommand(new FileOperationCommand() 
                {FileOperationType = FileOperationType.SymbolicLink, Source = source, Destination = destination});

        public static CopyFromLocalBuilder CopyFromLocal(string source, string destination, IExecutionContext context) =>
            new CopyFromLocalBuilder(context, source, destination);
        
        public static SetAttributesBuilder SetAttributes(string target, IExecutionContext context) =>
            new SetAttributesBuilder(context, target);
    }
}