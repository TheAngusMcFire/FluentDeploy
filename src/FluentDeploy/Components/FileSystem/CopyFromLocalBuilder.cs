using FluentDeploy.Enums;
using FluentDeploy.ExecutionUtils.Interfaces;

namespace FluentDeploy.Components.FileSystem
{
    public class CopyFromLocalBuilder : FileStateBuilderBase<CopyFromLocalBuilder>
    {
        private readonly string _source;
        public CopyFromLocalBuilder(IHostInfo info, string source,  string path) : base(info, path, FileOperationType.CopyFromLocal)
        {
            _source = source;
        }

        protected override void Execute(IExecutionContext context)
        {
            PrepareCommand(context);
            _fileOperationCommand.Source = _source;
            context.ExecuteCommand(_fileOperationCommand);
        }
    }
}