using Brazil.Api.Integration.Common;
using Brazil.Api.Integration.Interfaces;
using Brazil.Api.Integration.Repositories;
using Brazil.Api.Integration.Services;

namespace Brazil.Api.Integration.Configurations
{
    public static class DependencyInjection
    {
        public static void Ioc(this IServiceCollection services)
        {
            services
                .AddTransient<IHttpUtil, HttpUtil>()
                .AddTransient<IBookRepository, BookRepository>()
                .AddTransient<IBookService, BookService>()
                .AddTransient<ICompanyRepository, CompanyRepository>()
                .AddTransient<ICompanyService, CompanyService>();
        }
    }
}
