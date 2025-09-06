using Brazil.Api.Integration.Enums;

namespace Brazil.Api.Integration.Interfaces
{
    public interface IHttpUtil
    {
        Task<HttpResponseMessage> ExecuteAsync<Request>(
            Request request,
            HostBase hostBase,
            string uri,
            HttpMethod httpMethod,
            Dictionary<string, string>? headers = null);
    }
}