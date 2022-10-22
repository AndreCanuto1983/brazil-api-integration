using Microsoft.Net.Http.Headers;

namespace Brazil.Api.Integration.Configurations
{
    public static class HttpClientFactory
    {        
        public static void HttpClient(this WebApplicationBuilder builder)
        {
            builder.Services.AddHttpClient("BrazilApi", client =>
            {
                client.BaseAddress = new Uri(builder.Configuration.GetSection("Brazil-Api-Settings:Host").Value);
                
                client.DefaultRequestHeaders.Add(HeaderNames.Accept, "*/*");

                client.Timeout = TimeSpan.FromMilliseconds(
                    double.Parse(builder.Configuration.GetSection("Brazil-Api-Settings:Timeout").Value)
                    );               
            });
        }
    }
}
