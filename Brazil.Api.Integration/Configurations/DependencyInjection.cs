using Brazil.Api.Integration.Interfaces;
using Brazil.Api.Integration.Repositories;
using Brazil.Api.Integration.Services;

namespace Brazil.Api.Integration.Configurations
{
    public static class DependencyInjection
    {
        public static void Di(this IServiceCollection services)
        {
            services.AddTransient<IBookRepository, BookRepository>();            
            services.AddTransient<IBookService, BookService>();            
            services.AddTransient<ICompanyRepository, CompanyRepository>();            
            services.AddTransient<ICompanyService, CompanyService>();            
        }
    }
}
