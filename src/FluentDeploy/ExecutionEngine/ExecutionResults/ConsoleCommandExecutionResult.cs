using System;
using Serilog;

namespace FluentDeploy.ExecutionEngine.ExecutionResults
{
    public class ConsoleCommandExecutionResult : CommandExecutionResult
    {
        public int ReturnCode { get; set; }
        public string StdOutText { get; set; }
        public string StdErrText { get; set; }

        public override void PrintResultData(Action<string> printFunction)
        {
            printFunction($"ReturnCode: {ReturnCode}");
            printFunction($"StdOutText: {StdOutText}");
            printFunction($"StdErrText: {StdErrText}");
        }
    }
}