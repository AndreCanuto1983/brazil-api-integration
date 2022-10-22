using System.Text.Json.Serialization;

namespace Brazil.Api.Integration.Models.CompanyService
{
    public class CompanyResponse
    {
        public bool Success { get; set; }
        public string? Message { get; set; }

        [JsonPropertyName("Empresa")]
        public Company? Company { get; set; }
    }
}
