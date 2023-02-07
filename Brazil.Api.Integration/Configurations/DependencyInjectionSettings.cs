using Brazil.Api.Integration.Interfaces;
using Brazil.Api.Integration.Repositories;
using Brazil.Api.Integration.Services;

namespace Brazil.Api.Integration.Configurations
{
    public static class DependencyInjectionSettings
    {
        public static void DependencyInjection(this IServiceCollection services)
        {
            services.AddTransient<IBookRepository, BookRepository>();            
            services.AddTransient<IBookService, BookService>();            
            services.AddTransient<ICompanyRepository, CompanyRepository>();            
            services.AddTransient<ICompanyService, CompanyService>();            
        }
    }
}
