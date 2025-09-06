using Brazil.Api.Integration.Enums;
using Brazil.Api.Integration.Interfaces;
using Brazil.Api.Integration.Map;
using Brazil.Api.Integration.Models.CompanyService;
using System.Net;
using System.Text.Json;

namespace Brazil.Api.Integration.Services
{
    public class CompanyService(
        ILogger<CompanyService> logger,
        IHttpUtil httpUtil,
        ICompanyRepository companyRepository) : ICompanyService
    {
        private readonly ILogger<CompanyService> _logger = logger;
        private readonly IHttpUtil _httpUtil = httpUtil;
        private readonly ICompanyRepository _companyRepository = companyRepository;

        public async Task<CompanyResponse> GetCompanyMinhaReceitaApiAsync(string cnpj, CancellationToken cancellationToken)
        {
            try
            {
                var comapanyInRedis = await _companyRepository.GetCompanyAsync(cnpj, cancellationToken);

                if (comapanyInRedis?.Cnpj is not null)
                    comapanyInRedis.CompanyResponse(HttpStatusCode.OK);

                var httpResponse = await _httpUtil.ExecuteAsync(
                    string.Empty,
                    HostBase.MinhaReceita,
                    $"/{cnpj}",
                    HttpMethod.Get);

                var company = await JsonSerializer.DeserializeAsync<Company>(
                    await httpResponse.Content.ReadAsStreamAsync(cancellationToken),
                        new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true
                        }, cancellationToken);

                if (httpResponse.IsSuccessStatusCode)
                    _ = Task.Run(() => _companyRepository.SetCompanyAsync(company!, cancellationToken));

                return company!.CompanyResponse(
                    (int)httpResponse.StatusCode == (int)HttpStatusCode.BadRequest ?
                    HttpStatusCode.NoContent :
                    httpResponse.StatusCode);
            }
            catch (Exception ex)
            {
                _logger.LogError("[GetCompanyMinhaReceitaApiAsync][Exception]: {ex}", ex.Message);
                return ex.MapToErrorResponse<CompanyResponse>();
            }
        }

        public async Task<CompanyResponse> GetCompanyBrasilApiAsync(string cnpj, CancellationToken cancellationToken)
        {
            try
            {
                var comapanyInRedis = await _companyRepository.GetCompanyAsync(cnpj, cancellationToken);

                if (comapanyInRedis?.Cnpj is not null)
                    comapanyInRedis.CompanyResponse(HttpStatusCode.OK);

                var httpResponse = await _httpUtil.ExecuteAsync(
                    string.Empty,
                    HostBase.BrazilApi,
                    $"/api/cnpj/v1/{cnpj}",
                    HttpMethod.Get);

                var company = await JsonSerializer.DeserializeAsync<Company>(
                    await httpResponse.Content.ReadAsStreamAsync(cancellationToken),
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    }, cancellationToken);

                if (httpResponse.StatusCode == HttpStatusCode.OK)
                    _ = Task.Run(() => _companyRepository.SetCompanyAsync(company!, cancellationToken));

                return company!.CompanyResponse(
                    (int)httpResponse.StatusCode == (int)HttpStatusCode.BadRequest ?
                    HttpStatusCode.NoContent :
                    httpResponse.StatusCode);
            }
            catch (Exception ex)
            {
                _logger.LogError("[GetCompanyBrasilApiAsync][Exception]: {ex}", ex.Message);
                return ex.MapToErrorResponse<CompanyResponse>();
            }
        }
    }
}