using Brazil.Api.Integration.Enums;
using Brazil.Api.Integration.Interfaces;
using Microsoft.OpenApi.Extensions;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace Brazil.Api.Integration.Common
{
    public class HttpUtil(
        ILogger<HttpUtil> logger,
        IHttpClientFactory httpClientFactory) : IHttpUtil
    {
        private readonly ILogger<HttpUtil> _logger = logger;
        private readonly IHttpClientFactory _httpClientFactory = httpClientFactory;

        public async Task<HttpResponseMessage> ExecuteAsync<Request>(
            Request request,
            HostBase hostBase,
            string uri,
            HttpMethod httpMethod,
            Dictionary<string, string> headers = null)
        {
            var client = _httpClientFactory.CreateClient(hostBase.GetDisplayName());

            var httpRequest = MountHttpRequest(
                request,
                client.BaseAddress?.OriginalString ?? string.Empty,
                uri,
                httpMethod,
                headers);

            var httpResponse = await client.SendAsync(httpRequest);

            if (!httpResponse.IsSuccessStatusCode)
            {
                _logger.LogError("------Error Detail--------");

                _logger.LogInformation("[Route request]: {Host}", client.BaseAddress + uri);
                _logger.LogInformation("[Data request]: {Request}", JsonSerializer.Serialize(request));
                _logger.LogInformation("[Status Code]: {statusCode} | [Response]: {stringContent}",
                    (int)httpResponse.StatusCode,
                    await httpResponse.Content.ReadAsStringAsync());

                _logger.LogError("--------------------------");
            }

            return httpResponse;
        }

        private static HttpRequestMessage MountHttpRequest<Request>(
            Request request,
            string baseUrl,
            string uri,
            HttpMethod httpMethod,
            Dictionary<string, string>? headers = null)
        {
            baseUrl = (baseUrl.EndsWith('/')) ? baseUrl.TrimEnd('/') : baseUrl;

            var host = string.Concat(baseUrl.Trim(), uri.Trim());

            var httpRequest = ToRequestMessage(
                new Uri(host),
                httpMethod,
                httpMethod.Method.Equals(nameof(HttpMethod.Get), StringComparison.CurrentCultureIgnoreCase) ? string.Empty : JsonSerializer.Serialize(request),
                headers);

            return httpRequest;
        }

        private static HttpRequestMessage ToRequestMessage(
            Uri endpoint,
            HttpMethod httpMethod,
            string requestContent,
            Dictionary<string, string>? headers = null)
        {
            const string defaultContentType = "application/json";

            Uri? uriGet = null;

            if (httpMethod.Method.Equals(nameof(HttpMethod.Get), StringComparison.CurrentCultureIgnoreCase))
                uriGet = new Uri(endpoint.AbsoluteUri);

            var httpRequest = new HttpRequestMessage
            {
                RequestUri = httpMethod != HttpMethod.Get ? endpoint : uriGet,
                Method = httpMethod,
                Content = new StringContent(requestContent, Encoding.UTF8, defaultContentType)
            };

            httpRequest.Content.Headers.ContentType = new MediaTypeHeaderValue(defaultContentType);

            httpRequest.Headers.Accept.ParseAdd(defaultContentType);

            if (headers != null)
            {
                foreach (var header in headers)
                {
                    httpRequest.Headers.Add($"{header.Key}", $"{header.Value}");
                }
            }

            return httpRequest;
        }
    }
}