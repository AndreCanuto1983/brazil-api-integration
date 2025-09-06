namespace Brazil.Api.Integration.Configurations
{
    public static class CorsSettings
    {
        public static void Cors(this IServiceCollection services)
        {
            services.AddCors();

            services.AddCors(options =>
            {
                options.AddPolicy("allowSpecificOrigins",
                    policy =>
                    {
                        policy
                        .AllowAnyOrigin()
                        //.WithOrigins(
                        //    "http://localhost:8080",
                        //    "http://localhost:3000",
                        //    "http://192.168.0.195:8080",
                        //    "http://192.168.0.197:8080",
                        //    "https://pos-puc-front.web.app"
                        //    )
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                    });
            });
        }
    }
}
