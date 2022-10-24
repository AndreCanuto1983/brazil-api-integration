using Brazil.Api.Integration.Converter;
using Brazil.Api.Integration.Interfaces;
using Brazil.Api.Integration.Models;
using Brazil.Api.Integration.Models.CompanyService;
using System.Text.Json;

namespace Brazil.Api.Integration.Services
{
    public class CompanyService : ICompanyService
    {
        private const string PATH_NAME = "/api/cnpj/v1";
        private readonly ILogger<CompanyService> _logger;
        private readonly IHttpClientFactory _httpClientFactory;

        public CompanyService(
            ILogger<CompanyService> logger,
            IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<CompanyResponse> GetCompanyAsync(string cnpj, CancellationToken cancellationToken)
        {
            try
            {
                var client = _httpClientFactory.CreateClient("BrazilApi");

                var response = await client.GetAsync($"{PATH_NAME}/{cnpj}");

                if (response.IsSuccessStatusCode)
                {
                    var result = await JsonSerializer.DeserializeAsync<Company>(
                    await response.Content.ReadAsStreamAsync(),
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    }, cancellationToken);

                    return result!.Success();
                }

                var error = await JsonSerializer.DeserializeAsync<MessageError>(
                    await response.Content.ReadAsStreamAsync(),
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    }, cancellationToken);

                _logger.LogWarning("[CompanyService][GetCompanyAsync][Response]: {response}",
                await response.Content.ReadAsStringAsync());

                return error!.CompanyUnsuccessfully();
            }
            catch (Exception ex)
            {
                _logger.LogError("[CompanyService][GetCompanyAsync][Exception]: {ex}", ex.Message);
                throw;
            }
        }

    }
}
