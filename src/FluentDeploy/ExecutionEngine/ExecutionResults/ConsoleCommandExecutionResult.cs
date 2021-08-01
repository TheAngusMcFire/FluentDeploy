namespace FluentDeploy.ExecutionEngine
{
    public class ConsoleCommandExecutionResult : CommandExecutionResult
    {
        public int ReturnCode { get; set; }
        public string StdOutText { get; set; }
        public string StdErrText { get; set; }
    }
}