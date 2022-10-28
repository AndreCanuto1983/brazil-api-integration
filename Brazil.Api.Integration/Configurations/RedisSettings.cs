using Brazil.Api.Integration.Models.RedisSettings;
using StackExchange.Redis;

namespace Brazil.Api.Integration.Configurations
{
    public static class RedisSettings
    {
        public static void Redis(this WebApplicationBuilder builder)
        {
            var section = builder.Configuration.GetSection("Redis");
            var redisConfigs = section.Get<Redis>();

            builder.Services.AddStackExchangeRedisCache(options =>
            {
                options.InstanceName = redisConfigs.Instance;
                options.ConfigurationOptions = new ConfigurationOptions()
                {
                    EndPoints = { redisConfigs.Connection },
                    AbortOnConnectFail = false,
                    ReconnectRetryPolicy = new LinearRetry(1500),
                    ConnectRetry = redisConfigs.ConnectRetry,
                    ConnectTimeout = redisConfigs.ConnectTimeout,
                    AsyncTimeout = 10000,
                    Ssl = false,
                    DefaultDatabase = 0
                };
            });
        }
    }
}
