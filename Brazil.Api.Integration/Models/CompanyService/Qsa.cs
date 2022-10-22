using System.Text.Json.Serialization;

namespace Brazil.Api.Integration.Models.CompanyService
{
    public class Qsa
    {
        [JsonPropertyName("nome_socio")]
        public string PartnerName { get; set; }

        [JsonPropertyName("faixa_etaria")]
        public string AgeRange { get; set; }

        [JsonPropertyName("cnpj_cpf_do_socio")]
        public string CpnjCpfPartner { get; set; }

        [JsonPropertyName("codigo_qualificacao_socio")]
        public int? PartnerQualificationCode { get; set; }

        [JsonPropertyName("qualificacao_socio")]
        public string PartnerQualification { get; set; }

        [JsonPropertyName("qualificacao_representante_legal")]
        public string LegalRepresentativeQualification { get; set; }

        [JsonPropertyName("data_entrada_sociedade")]
        public string CompanyEntryDate { get; set; }

        [JsonPropertyName("cpf_representante_legal")]
        public string CpfLegalRepresentative { get; set; }

        [JsonPropertyName("nome_representante_legal")]
        public string LegalRepresentativeName { get; set; }
    }
}