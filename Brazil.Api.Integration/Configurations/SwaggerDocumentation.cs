using System.Reflection;

namespace Brazil.Api.Integration.Configurations
{
    public static class SwaggerDocumentation
    {
        public static void SwaggerSettings(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
            });
        }
    }
}
