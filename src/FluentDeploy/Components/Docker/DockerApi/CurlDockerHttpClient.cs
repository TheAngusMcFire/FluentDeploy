using System;
using System.IO;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using FluentDeploy.Components.Curl;
using FluentDeploy.ExecutionUtils.Interfaces;

namespace FluentDeploy.Components.Docker.DockerApi
{
    public class CurlDockerHttpClient : IDockerHttpClient
    {
        private readonly bool _asRoot;
        private readonly IExecutionContext _executionContext;
        public int Timeout { get; set; } = 60;

        public CurlDockerHttpClient(IExecutionContext executionContext, bool asRoot = false)
        {
            _asRoot = asRoot;
            _executionContext = executionContext;
        }

        public Task Delete(string url, int expectedReturnCode)
        {
            var curl = GetCurlCommand(url, HttpMethod.Delete);

            if (_asRoot)
                curl.RunAsRoot();

            curl.ExecuteOn(_executionContext);

            if (curl.HttpStatusCode != expectedReturnCode)
                throw new InvalidOperationException(
                    $"Error unexpected return code: {curl.HttpStatusCode} but should be: {expectedReturnCode}");
            
            return Task.CompletedTask;
        }

        public Task<T> Get<T>(string url, int expectedReturnCode) where T : class
        {
            var curl = GetCurlCommand(url, HttpMethod.Get);

            if (_asRoot)
                curl.RunAsRoot();

            curl.ExecuteOn(_executionContext);

            File.WriteAllText("/tmp/get.json", curl.Response);

            if(curl.HttpStatusCode == 404)
                return Task.FromResult<T>(null);

            if (curl.HttpStatusCode != expectedReturnCode)
                throw new InvalidOperationException(
                    $"Error unexpected return code: {curl.HttpStatusCode} but should be: {expectedReturnCode}");

            return Task.FromResult(JsonSerializer.Deserialize<T>(curl.Response));
        }

        public Task<T> Post<T>(string url, object payload, int expectedReturnCode) where T : class
        {
            var curl = GetCurlCommand(url, HttpMethod.Post)
                .AddHeader("Content-Type", "application/json")
                .WithBody(JsonSerializer.Serialize(payload));

            if (_asRoot)
                curl.RunAsRoot();

            curl.ExecuteOn(_executionContext);

            if (curl.HttpStatusCode == 404 || curl.HttpStatusCode == 304 )
                return Task.FromResult<T>(null);

            if (curl.HttpStatusCode != expectedReturnCode)
                throw new InvalidOperationException(
                    $"Error unexpected return code: {curl.HttpStatusCode} but should be: {expectedReturnCode}: response msg: {curl.Response}");

            if (typeof(T) == typeof(string))
            {
                return Task.FromResult(curl.Response as T);
            }

            return Task.FromResult(JsonSerializer.Deserialize<T>(curl.Response));
        }

        private CurlCommandBuilder GetCurlCommand(string url, HttpMethod method)
        {
            return new(url, method, "/var/run/docker.sock"){SuppressOutput = true, Timeout = Timeout};
        }
    }
}