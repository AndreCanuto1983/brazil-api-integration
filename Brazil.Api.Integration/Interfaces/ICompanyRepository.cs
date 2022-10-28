using Brazil.Api.Integration.Models.CompanyService;

namespace Brazil.Api.Integration.Interfaces
{
    public interface ICompanyRepository
    {
        Task SetCompanyAsync(Company company, CancellationToken cancellationToken);
        Task<Company?> GetCompanyAsync(string cnpj, CancellationToken cancellationToken);
    }
}
