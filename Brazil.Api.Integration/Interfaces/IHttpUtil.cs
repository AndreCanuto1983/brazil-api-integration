using Brazil.Api.Integration.Enums;

namespace Brazil.Api.Integration.Interfaces
{
    public interface IHttpUtil
    {
        Task<HttpResponseMessage> GetAsync(
            HostBase hostBase,
            string uri,
            bool showRequest = true,
            bool showResponse = true);

        Task<HttpResponseMessage> ExecuteAsync<Request>(
            Request request,
            HostBase hostBase,
            string uri,
            HttpMethod httpMethod,
            Dictionary<string, string>? headers = null,
            bool showRequest = true,
            bool showResponse = true);
    }
}
