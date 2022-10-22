using System.Text.Json.Serialization;

namespace Brazil.Api.Integration.Models.CompanyService
{
    public class Cnae
    {
        [JsonPropertyName("codigo")]
        public int? Code { get; set; }

        [JsonPropertyName("descricao")]
        public string Description { get; set; }
    }
}