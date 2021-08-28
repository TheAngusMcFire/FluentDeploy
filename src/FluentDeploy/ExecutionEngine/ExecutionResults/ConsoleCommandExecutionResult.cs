using System;
using Serilog;

namespace FluentDeploy.ExecutionEngine.ExecutionResults
{
    public class ConsoleCommandExecutionResult : CommandExecutionResult
    {
        public string CommandLine { get; set; }
        public int ReturnCode { get; set; }
        public string StdOutText { get; set; }
        public string StdErrText { get; set; }

        public override void PrintResultData(Action<string> printFunction)
        {
            printFunction($"CommandLine: {CommandLine}");
            printFunction($"ReturnCode: {ReturnCode}");
            printFunction($"StdOutText: {StdOutText}");
            printFunction($"StdErrText: {StdErrText}");
        }
    }
}