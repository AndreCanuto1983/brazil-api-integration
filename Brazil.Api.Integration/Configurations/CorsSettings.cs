namespace Brazil.Api.Integration.Configurations
{
    public static class CorsSettings
    {
        public static void Cors(this IServiceCollection services)
        {
            services.AddCors();

            //var allowSpecificOrigins = "MyPolicy";            

            //services.AddCors(options =>
            //{
            //    options.AddPolicy(allowSpecificOrigins,
            //        policy =>
            //        {
            //            policy.WithOrigins(
            //                "http://localhost:8080",
            //                "http://localhost:3000",
            //                "http://192.168.0.195:8080",
            //                "http://192.168.0.197:8080",
            //                "https://pos-puc-front.web.app"
            //                )
            //            .AllowAnyHeader()
            //            .AllowAnyMethod();
            //        });
            //});
        }
    }
}
