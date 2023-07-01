using Brazil.Api.Integration.Enums;
using Microsoft.Net.Http.Headers;
using Microsoft.OpenApi.Extensions;

namespace Brazil.Api.Integration.Configurations
{
    public static class HttpClientFactorySettings
    {        
        public static void HttpClientFactory(this WebApplicationBuilder builder)
        {
            builder.Services.AddHttpClient(HostBase.BrazilApi.GetDisplayName(), client =>
            {
                client.BaseAddress = new Uri(builder.Configuration.GetSection("Brazil-Api-Settings:Host").Value);                
                client.DefaultRequestHeaders.Add(HeaderNames.Accept, "*/*");
                client.Timeout = TimeSpan.FromMilliseconds(
                    double.Parse(builder.Configuration.GetSection("Brazil-Api-Settings:Timeout").Value));               
            });

            builder.Services.AddHttpClient(HostBase.MinhaReceita.GetDisplayName(), client =>
            {
                client.BaseAddress = new Uri(builder.Configuration.GetSection("Minha-Receita-Api-Settings:Host").Value);

                client.DefaultRequestHeaders.Add(HeaderNames.Accept, "*/*");

                client.Timeout = TimeSpan.FromMilliseconds(
                    double.Parse(builder.Configuration.GetSection("Minha-Receita-Api-Settings:Timeout").Value)
                    );
            });
        }
    }
}
