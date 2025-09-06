using Brazil.Api.Integration.Interfaces;
using Brazil.Api.Integration.Models.CompanyService;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace Brazil.Api.Integration.Repositories
{
    public class CompanyRepository(
        IDistributedCache distributedCache,
        ILogger<CompanyRepository> logger) : ICompanyRepository
    {
        private readonly IDistributedCache _distributedCache = distributedCache;
        private readonly ILogger<CompanyRepository> _logger = logger;

        public async Task SetCompanyAsync(Company company, CancellationToken cancellationToken)
        {
            try
            {
                if (company.Cnpj is null)
                    return;

                var response = await _distributedCache.GetAsync(company.Cnpj, cancellationToken);

                if (response != null)
                    return;

                await _distributedCache.SetStringAsync(
                    company.Cnpj,
                    JsonSerializer.Serialize(company),
                    cancellationToken);
            }
            catch (Exception ex)
            {
                _logger.LogError("[SetCompanyAsync] -> EXCEPTION: {ex}", ex.Message);
            }
        }

        public async Task<Company> GetCompanyAsync(string cnpj, CancellationToken cancellationToken)
        {
            try
            {
                var response = await _distributedCache.GetAsync(cnpj, cancellationToken);

                if (response == null)
                    return new Company();

                return JsonSerializer.Deserialize<Company>(response);
            }
            catch (Exception ex)
            {
                _logger.LogError("[GetCompanyAsync] -> EXCEPTION: {ex}", ex.Message);
                return new Company();
            }
        }
    }
}
