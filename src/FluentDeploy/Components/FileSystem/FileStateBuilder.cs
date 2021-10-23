using System.IO;
using System.Text;
using System.Text.Json;
using FluentDeploy.Enums;
using FluentDeploy.ExecutionUtils.Interfaces;
using Stubble.Core.Builders;

namespace FluentDeploy.Components.FileSystem
{
    public class FileStateBuilder : FileStateBuilderBase<FileStateBuilder>
    {
        public FileStateBuilder Content(string content) => 
            FluentExec(() => _fileOperationCommand.FileContent = Encoding.Default.GetBytes(content));
        
        public FileStateBuilder Content(byte[] content) => 
            FluentExec(() => _fileOperationCommand.FileContent = content);

        public FileStateBuilder ContentFromFile(string path) => 
            FluentExec(() => _fileOperationCommand.FileContent = File.ReadAllBytes(path));

        public FileStateBuilder ContentFromTemplate(string path, object context)
        {
            var stubble = new StubbleBuilder().Build();
            var file = File.ReadAllText(path);
            var content = stubble.Render(file, context);
            Content(content);
            return this;
        }

        public FileStateBuilder(IHostInfo info, string path) : base(info, path, FileOperationType.CreateFile)
        { }

        protected override void Execute(IExecutionContext context)
        {
            PrepareCommand(context);
            context.ExecuteCommand(_fileOperationCommand);
        }
    }
}