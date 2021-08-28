using System;
using System.Collections.Generic;
using System.Net.Http;
using FluentDeploy.Commands;
using FluentDeploy.ExecutionUtils.Interfaces;
using FluentDeploy.Extentions;

namespace FluentDeploy.Components.Curl
{
    public class CurlCommandBuilder : BaseCommandBuilder<CurlCommandBuilder>
    {
        private Dictionary<string, string> _headerValues = new();
        public int Timeout { get; set; } = 60;
        private readonly HttpMethod _method;
        private readonly string _url;
        private string _body;
        private string _response;
        private int _returnCode;
        private readonly string _unixSocket;

        public CurlCommandBuilder(string url, HttpMethod method, string unixSocket = null)
        {
            _url = url;
            _method = method;
            _unixSocket = unixSocket;
        }

        public CurlCommandBuilder AddHeader(string name, string value) => 
            FluentExec(() => _headerValues.Add(name, value));
        
        public CurlCommandBuilder WithBody(string body) => 
            FluentExec(() => _body = body);

        protected override void Execute(IExecutionContext executor)
        {
            var args = new List<string> {"-X", _method.Method, "-w", "%{http_code}"};

            if (_unixSocket != null)
            {
                args.Add("--unix-socket");
                args.Add(_unixSocket);
            }

            foreach (var keyValuePair in _headerValues)
            {
                args.Add("-H");
                args.Add($"{keyValuePair.Key}: {keyValuePair.Value}");
            }

            if (_body != null)
            {
                args.Add("-d");
                args.Add(_body);
            }
            
            args.Add(_url);

            var cmd = ConsoleCommand.Exec("curl")
                .WithArguments(args.ToArray()); 
            cmd.Timeout = Timeout;
            var result = executor.ExecuteConsoleCommand(cmd);
            var output = result.StdOutText;
            var lastLineIdx = output.LastIndexOf(Environment.NewLine, StringComparison.Ordinal);
            if (lastLineIdx == -1)
            {
                // only one line
                _returnCode = Convert.ToInt32(output.Trim());
                _response = "";
            }
            else
            {
                _returnCode = Convert.ToInt32(output.Substring(lastLineIdx).Trim());
                _response = output.Remove(lastLineIdx);
            }
        }

        public string Response => _response;
        public int HttpStatusCode => _returnCode;
    }
}