using FluentDeploy.Enums;
using FluentDeploy.ExecutionUtils.Interfaces;

namespace FluentDeploy.Components.FileSystem
{
    public class DirectoryStateBuilder : FileStateBuilderBase<DirectoryStateBuilder>
    {
        public DirectoryStateBuilder(IHostInfo info, string path) : base(info, path, FileOperationType.CreateDirectory)
        {
            
        }

        protected override void Execute(IExecutionContext context)
        {
            PrepareCommand(context);
            context.ExecuteCommand(_fileOperationCommand);
        }
    }
}