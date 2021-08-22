using FluentDeploy.Enums;

namespace FluentDeploy.Commands
{
    public class FileOperationCommand
    {
        public FileOperationType FileOperationType { get; set; }
        public string Source { get; set; }
        public string Destination { get; set; }
        public string FileContent { get; set; }
    }
}