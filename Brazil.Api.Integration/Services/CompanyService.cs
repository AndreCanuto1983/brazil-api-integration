using Brazil.Api.Integration.Converter;
using Brazil.Api.Integration.Enums;
using Brazil.Api.Integration.Interfaces;
using Brazil.Api.Integration.Models;
using Brazil.Api.Integration.Models.CompanyService;
using System.Text.Json;

namespace Brazil.Api.Integration.Services
{
    public class CompanyService : ICompanyService
    {        
        private readonly ILogger<CompanyService> _logger;
        private readonly IHttpUtil _httpUtil;
        private readonly ICompanyRepository _companyRepository;

        public CompanyService(
            ILogger<CompanyService> logger,
            IHttpUtil httpUtil,
            ICompanyRepository companyRepository)
        {
            _logger = logger;
            _httpUtil = httpUtil;
            _companyRepository = companyRepository;
        }

        public async Task<CompanyResponse> GetCompanyAsync(string cnpj, CancellationToken cancellationToken)
        {
            try
            {
                var comapanyInRedis = await _companyRepository.GetCompanyAsync(cnpj, cancellationToken);

                if (comapanyInRedis is not null)
                    comapanyInRedis.Success();

                var httpResponse = await _httpUtil.GetAsync(HostBase.BrazilApi, $"/api/cnpj/v1/{cnpj}");

                if (httpResponse.IsSuccessStatusCode)
                {
                    var company = await JsonSerializer.DeserializeAsync<Company>(
                    await httpResponse.Content.ReadAsStreamAsync(),
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    }, cancellationToken);

                    await _companyRepository.SetCompanyAsync(company!, cancellationToken);

                    return company!.Success();
                }

                var error = await JsonSerializer.DeserializeAsync<MessageError>(
                    await httpResponse.Content.ReadAsStreamAsync(),
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
