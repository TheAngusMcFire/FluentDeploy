using System;
using System.Collections.Generic;
using System.Text.Json;
using FluentDeploy.Components.Docker.DockerApi.Model;

namespace FluentDeploy.Components.Docker.DockerApi
{
    public class DockerApi
    {
        private const string DockerBaseUrl = "http://localhost";
        private const string DockerVersion = "v1.40";
        private readonly IDockerHttpClient _client;
        private string DockerUrl = $"{DockerBaseUrl}/{DockerVersion}";

        protected class CreateDockerResponse
        {
            public string Id { get; set; }
        }

        public DockerApi(IDockerHttpClient client)
        {
            _client = client;
        }

        public void SetDockerBaseUrl(string baseUrl)
        {
            DockerUrl = baseUrl;
        }

        public List<ContainerSummary> GetContainers(bool all)
        {
            return _client.Get<List<ContainerSummary>>($"{DockerUrl}/containers/json?all={all}", 200).Result;
        }

        public ContainerInspectResponse InspectContainer(string nameOrId)
        {
            return _client.Get<ContainerInspectResponse>($"{DockerUrl}/containers/{nameOrId}/json", 200).Result;
        }

        public string CreateContainer(string name, ContainerConfig config)
        {
            var resp = _client.Post<string>($"{DockerUrl}/containers/create?name={name}", config, 201).Result;
            return JsonSerializer.Deserialize<CreateDockerResponse>(resp)?.Id;
        }

        public string RenameContainer(string nameOrId, string name)
        {
            return _client.Post<string>($"{DockerUrl}/containers/{nameOrId}/rename?name={name}", new object(), 204).Result;
        }
        
        public string StopContainer(string nameOrId)
        {
            return _client.Post<string>($"{DockerUrl}/containers/{nameOrId}/stop", new object(), 204).Result;
        }
        
        public string StartContainer(string nameOrId)
        {
            return _client.Post<string>($"{DockerUrl}/containers/{nameOrId}/start", null, 204).Result;
        }
        
        public string RestartContainer(string nameOrId)
        {
            return _client.Post<string>($"{DockerUrl}/containers/{nameOrId}/restart", null, 204).Result;
        }
        
        public string PruneImages(bool dangling)
        {
            return _client.Post<string>($"{DockerUrl}/images/prune?dangling={dangling}", new object(), 200).Result;
        }
        
        public void DeleteContainer(string nameOrId)
        {
            _client.Delete($"{DockerUrl}/containers/{nameOrId}", 204);
        }

        public List<Network> GetNetworks()
        {
            return _client.Get<List<Network>>($"{DockerUrl}/networks", 200).Result;
        }

        public NetworkCreateResponse CreateNetwork(string name)
        {
            var req = new InlineObject2(name, true);
            return _client.Post<NetworkCreateResponse>($"{DockerUrl}/networks/create", req, 201).Result;
        }
        
        public string ConnectToNetwork(string containerId, string networkId)
        {
            var req = new InlineObject3(containerId);
            return _client.Post<string>($"{DockerUrl}/networks/{networkId}/connect", req, 200).Result;
        }

        public string PullImage(string image, string authToken)
        {
            return _client.Post<string>($"{DockerUrl}/images/create?fromImage={image}", new object(), 200, authToken).Result;
        }
        
        public Image InspectImage(string nameOrId)
        {
            return _client.Get<Image>($"{DockerUrl}/images/{nameOrId}/json", 200).Result;
        }
        
        public List<ImageSummary> ListImages(bool all, string imageFilter)
        {
            return _client.Get<List<ImageSummary>>($"{DockerUrl}/images/json?all={all}&reference={imageFilter}", 200).Result;
        }
    }
}