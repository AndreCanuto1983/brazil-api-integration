using Brazil.Api.Integration.Converter;
using Brazil.Api.Integration.Enums;
using Brazil.Api.Integration.Interfaces;
using Brazil.Api.Integration.Models;
using Brazil.Api.Integration.Models.CompanyService;
using Microsoft.OpenApi.Extensions;
using System.Text.Json;

namespace Brazil.Api.Integration.Services
{
    public class CompanyService : ICompanyService
    {        
        private readonly ILogger<CompanyService> _logger;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ICompanyRepository _companyRepository;

        public CompanyService(
            ILogger<CompanyService> logger,
            IHttpClientFactory httpClientFactory,
            ICompanyRepository companyRepository)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;
            _companyRepository = companyRepository;
        }

        public async Task<CompanyResponse> GetCompanyAsync(string cnpj, CancellationToken cancellationToken)
        {
            try
            {
                var comapanyInRedis = await _companyRepository.GetCompanyAsync(cnpj, cancellationToken);

                if (comapanyInRedis is not null)
                    comapanyInRedis.Success();

                var client = _httpClientFactory.CreateClient(Hosts.BrazilApi.GetDisplayName());

                var response = await client.GetAsync($"/api/cnpj/v1/{cnpj}");

                _logger.LogInformation("[CompanyService][GetCompanyAsync] => STATUS CODE: {statusCode}, RESPONSE: {response}",
                    (int)response.StatusCode, await response.Content.ReadAsStringAsync());

                if (response.IsSuccessStatusCode)
                {
                    var company = await JsonSerializer.DeserializeAsync<Company>(
                    await response.Content.ReadAsStreamAsync(),
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    }, cancellationToken);

                    await _companyRepository.SetCompanyAsync(company, cancellationToken);

                    return company!.Success();
                }

                var error = await JsonSerializer.DeserializeAsync<MessageError>(
                    await response.Content.ReadAsStreamAsync(),
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    }, cancellationToken);

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
