using FluentDeploy.Enums;
using FluentDeploy.ExecutionUtils.Interfaces;

namespace FluentDeploy.Components.FileSystem
{
    public class DirectoryStateBuilder : FileStateBuilderBase
    {
        public DirectoryStateBuilder(IHostInfo info, string path) : base(info, path, FileOperationType.CreateDirectory)
        {
            
        }

        protected override void Execute(IExecutionContext executor) => 
            executor.ExecuteCommand(_fileOperationCommand);
    }
}