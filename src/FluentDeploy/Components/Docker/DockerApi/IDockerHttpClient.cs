using System.Threading.Tasks;

namespace FluentDeploy.Components.Docker.DockerApi
{
    public interface IDockerHttpClient
    {
        Task Delete(string url, int expectedReturnCode);
        Task<T> Get<T>(string url, int expectedReturnCode) where T : class;
        Task<T> Post<T>(string url, object payload, int expectedReturnCode, string authToken = null) where T : class;
    }
}