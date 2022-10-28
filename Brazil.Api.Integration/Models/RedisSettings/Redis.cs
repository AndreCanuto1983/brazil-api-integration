namespace Brazil.Api.Integration.Models.RedisSettings
{
    public class Redis
    {
        public string Instance { get; set; }
        public string Connection { get; set; }
        public int ConnectRetry { get; set; }
        public int ConnectTimeout { get; set; }
    }
}
