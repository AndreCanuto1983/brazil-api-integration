using Brazil.Api.Integration.Models.CompanyService;

namespace Brazil.Api.Integration.Interfaces
{
    public interface ICompanyService
    {
        Task<CompanyResponse> GetCompanyAsync(string cnpj, CancellationToken cancellationToken);
    }
}
