using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Net.Sockets;
using System.Text.Json;
using System.Threading.Tasks;

namespace FluentDeploy.Components.Docker.DockerApi
{
    public class UnixSocketDockerHttpClient : IDockerHttpClient
    {
        private const string DockerSocketPath = "/var/run/docker.sock";
        
        private readonly HttpClient _httpClient = new HttpClient(new SocketsHttpHandler
        {
            ConnectCallback = async (context, token) =>
            {
                var socket = new Socket(AddressFamily.Unix, SocketType.Stream, ProtocolType.IP);
                var endpoint = new UnixDomainSocketEndPoint(DockerSocketPath);
                await socket.ConnectAsync(endpoint);
                return new NetworkStream(socket, ownsSocket: true);
            }
        });

        private void ValidateReturnCode(HttpStatusCode actual, int expected)
        {
            if (actual != (HttpStatusCode) expected)
                throw new InvalidOperationException(
                    $"Error unexpected return code: {actual} but should be: {expected}");
        }

        public async Task Delete(string url, int expectedReturnCode)
        {
            var res = await _httpClient.DeleteAsync(url);
            ValidateReturnCode(res.StatusCode, expectedReturnCode);
        }

        public async Task<T> Get<T>(string url, int expectedReturnCode) where T : class
        {
            var res = await _httpClient.GetAsync(url);
            if(res.StatusCode == HttpStatusCode.NotFound)
                return null;
            ValidateReturnCode(res.StatusCode, expectedReturnCode);
            return await JsonSerializer.DeserializeAsync<T>(await res.Content.ReadAsStreamAsync());
        }

        public async Task<T> Post<T>(string url, object payload, int expectedReturnCode, string authToken) where T : class
        {
            var requestMessage = new HttpRequestMessage(HttpMethod.Post, url);
            
            if (authToken != null)
            {
                requestMessage.Headers.Add("X-Registry-Auth", authToken);
            }

            requestMessage.Content = JsonContent.Create(payload);

            var res = await _httpClient.SendAsync(requestMessage);
            
            if(res.StatusCode == HttpStatusCode.NotFound || res.StatusCode == HttpStatusCode.NotModified)
                return null;
            
            ValidateReturnCode(res.StatusCode, expectedReturnCode);
            
            if (typeof(T) == typeof(string))
                return await res.Content.ReadAsStringAsync() as T;

            return await JsonSerializer.DeserializeAsync<T>(await res.Content.ReadAsStreamAsync());
        }
    }
}