using Brazil.Api.Integration.Models.Base;
using System.Text.Json.Serialization;

namespace Brazil.Api.Integration.Models.CompanyService
{
    public class Company
    {
        public string? Cnpj { get; set; }

        [JsonPropertyName("razao_social")]
        public string? CorporateName { get; set; }

        [JsonPropertyName("nome_fantasia")]
        public string? FantasyName { get; set; }

        [JsonPropertyName("codigo_natureza_juridica")]
        public int? LegalNatureCode { get; set; }

        [JsonPropertyName("natureza_juridica")]
        public string? LegalNature { get; set; }

        [JsonPropertyName("situacao_cadastral")]
        public int? RegistrationStatus { get; set; }

        [JsonPropertyName("descricao_situacao_cadastral")]
        public string? DescriptionRegistrationStatus { get; set; }

        [JsonPropertyName("data_situacao_cadastral")]
        public string? DateRegistrationStatus { get; set; }

        [JsonPropertyName("cnae_fiscal")]
        public int? CnaeFiscal { get; set; }

        [JsonPropertyName("cnae_fiscal_descricao")]
        public string? CnaeFiscalDescription { get; set; }

        [JsonPropertyName("capital_social")]
        public decimal? JointStock { get; set; }

        [JsonPropertyName("uf")]
        public string? Uf { get; set; }

        [JsonPropertyName("cep")]
        public string? ZipCode { get; set; }

        [JsonPropertyName("descricao_tipo_de_logradouro")]
        public string? TypeStreet { get; set; }

        [JsonPropertyName("logradouro")]
        public string? Street { get; set; }

        [JsonPropertyName("numero")]
        public string? Number { get; set; }

        [JsonPropertyName("complemento")]
        public string? AddressComplement { get; set; }

        [JsonPropertyName("bairro")]
        public string? Neighborhood { get; set; }

        [JsonPropertyName("municipio")]
        public string? County { get; set; }

        [JsonPropertyName("ddd_fax")]
        public string? Fax { get; set; }

        [JsonPropertyName("ddd_telefone_1")]
        public string? Telephone { get; set; }

        [JsonPropertyName("data_inicio_atividade")]
        public string? ActivityStartDate { get; set; }

        [JsonPropertyName("descricao_identificador_matriz_filial")]
        public string? BranchMatrixIdentifier { get; set; }

        [JsonPropertyName("data_opcao_pelo_mei")]
        public string? DateOptionByMei { get; set; }

        [JsonPropertyName("data_exclusao_do_mei")]
        public string? MeiExclusionDate { get; set; }

        [JsonPropertyName("qsa")]
        public List<Qsa>? Qsa { get; set; }

        [JsonPropertyName("cnaes_secundarios")]
        public List<Cnae>? Cnae { get; set; }

        public string? Message { get; set; }
    }
}
