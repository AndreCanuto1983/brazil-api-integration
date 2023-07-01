using Brazil.Api.Integration.Models.CompanyService;

namespace Brazil.Api.Integration.Interfaces
{
    public interface ICompanyService
    {
        Task<CompanyResponse> GetCompanyMinhaReceitaApiAsync(string cnpj, CancellationToken cancellationToken);
        Task<CompanyResponse> GetCompanyBrasilApiAsync(string cnpj, CancellationToken cancellationToken);
    }
}
