using FluentDeploy.Enums;
using FluentDeploy.ExecutionUtils.Interfaces;

namespace FluentDeploy.Components.FileSystem
{
    public class SetAttributesBuilder : FileStateBuilderBase<SetAttributesBuilder>
    {
        public SetAttributesBuilder(IHostInfo info, string path) : base(info, path, FileOperationType.SetAttributes)
        { }

        protected override void Execute(IExecutionContext context)
        {
            PrepareCommand(context);
            context.ExecuteCommand(_fileOperationCommand);
        }
    }
}