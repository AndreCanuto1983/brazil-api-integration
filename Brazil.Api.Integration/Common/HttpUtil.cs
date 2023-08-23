using Brazil.Api.Integration.Enums;
using Brazil.Api.Integration.Interfaces;
using Microsoft.OpenApi.Extensions;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace Brazil.Api.Integration.Common
{
    public class HttpUtil : IHttpUtil
    {
        private readonly ILogger<HttpUtil> _logger;
        private readonly IHttpClientFactory _httpClientFactory;

        public HttpUtil(
            ILogger<HttpUtil> logger,
            IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<HttpResponseMessage> GetAsync(
            HostBase hostBase,
            string uri,
            bool showRequest = true,
            bool showResponse = true)
        {
            var client = _httpClientFactory.CreateClient(hostBase.GetDisplayName());

            if (showRequest)
                _logger.LogInformation("[HttpUtil][GetAsync] => HOST: {host}", client.BaseAddress + uri);

            var httpResponse = await client.GetAsync(uri);

            if (showResponse)
                _logger.LogInformation("[HttpUtil][GetAsync] => STATUS CODE: {statusCode} | RESPONSE: {stringContent}",
                (int)httpResponse.StatusCode,
                    await httpResponse.Content.ReadAsStringAsync());

            return httpResponse;
        }

        public async Task<HttpResponseMessage> ExecuteAsync<Request>(
            Request request,
            HostBase hostBase,
            string uri,
            HttpMethod httpMethod,
            Dictionary<string, string>? headers = null,
            bool showRequest = true,
            bool showResponse = true)
        {
            var client = _httpClientFactory.CreateClient(hostBase.GetDisplayName());

            var httpRequest = MountHttpRequest(
                request,
                client.BaseAddress?.OriginalString ?? String.Empty,
                uri,
                httpMethod,
                headers);

            _logger.LogInformation("[HttpUtil][ExecuteAsync] => HOST: {host}", client.BaseAddress + uri);

            if (showRequest)
                _logger.LogInformation("[HttpUtil][ExecuteAsync] => REQUEST: {request}", JsonSerializer.Serialize(request));

            var httpResponse = await client.SendAsync(httpRequest);

            if (showResponse)
                _logger.LogInformation("[HttpUtil][ExecuteAsync] => STATUS CODE: {statusCode} | RESPONSE: {stringContent}",
                (int)httpResponse.StatusCode,
                    await httpResponse.Content.ReadAsStringAsync());

            return httpResponse;
        }

        private static HttpRequestMessage MountHttpRequest<Request>(
            Request request,
            string baseUrl,
            string uri,
            HttpMethod httpMethod,
            Dictionary<string, string>? headers = null)
        {
            var host = MountUrl(baseUrl, uri);

            var httpRequest = ToRequestMessage(
                new Uri(host),
                httpMethod,
                httpMethod.Method == HttpMethod.Get.ToString() ? string.Empty : JsonSerializer.Serialize(request),
                headers);

            return httpRequest;
        }

        private static string MountUrl(string baseUrl, string uri)
        {
            baseUrl = (baseUrl.EndsWith("/")) ? baseUrl.TrimEnd('/') : baseUrl;
            return string.Concat(baseUrl.Trim(), uri.Trim());
        }

        private static HttpRequestMessage ToRequestMessage(
                    Uri endpoint,
                    HttpMethod method,
                    string requestContent,
                    Dictionary<string, string>? headers = null)
        {
            Uri? uriGet = null;

            if (HttpMethod.Get == method)
                uriGet = new Uri(endpoint.AbsoluteUri);

            const string defaultContentType = "application/json";

            var httpRequest = new HttpRequestMessage
            {
                RequestUri = method != HttpMethod.Get ? endpoint : uriGet,
                Method = method,
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
