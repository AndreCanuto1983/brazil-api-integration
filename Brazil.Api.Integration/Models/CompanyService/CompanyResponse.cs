using Brazil.Api.Integration.Interfaces;
using Brazil.Api.Integration.Models.Base;
using System.Text.Json.Serialization;

namespace Brazil.Api.Integration.Models.CompanyService
{
    public class CompanyResponse : IResponseBase
    {
        [JsonIgnore]
        public bool Success { get; set; }

        [JsonPropertyName("Empresa")]
        public Company? Company { get; set; }

        [JsonIgnore]
        public int? StatusCode { get; set; }

        [JsonIgnore]
        public MessageResponse? Message { get; set; }
    }
}
