using System.Text.Json.Serialization;

namespace Brazil.Api.Integration.Configurations
{
    public static class JsonExtensionSettings
    {
        public static void JsonSettings(this IServiceCollection services)
        {
            services.AddControllers()
                    .AddJsonOptions(options =>
                    {
                        options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
                        options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault;
                    });
        }
    }
}
