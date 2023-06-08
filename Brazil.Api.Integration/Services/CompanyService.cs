﻿using Brazil.Api.Integration.Converter;
using Brazil.Api.Integration.Enums;
using Brazil.Api.Integration.Interfaces;
using Brazil.Api.Integration.Models.CompanyService;
using System.Net;
using System.Text.Json;

namespace Brazil.Api.Integration.Services
{
    public class CompanyService : ICompanyService
    {
        private readonly ILogger<CompanyService> _logger;
        private readonly IHttpUtil _httpUtil;
        private readonly ICompanyRepository _companyRepository;

        public CompanyService(
            ILogger<CompanyService> logger,
            IHttpUtil httpUtil,
            ICompanyRepository companyRepository)
        {
            _logger = logger;
            _httpUtil = httpUtil;
            _companyRepository = companyRepository;
        }

        public async Task<CompanyResponse> GetCompanyAsync(string cnpj, CancellationToken cancellationToken)
        {
            try
            {
                var comapanyInRedis = await _companyRepository.GetCompanyAsync(cnpj, cancellationToken);

                if (comapanyInRedis is not null)
                    comapanyInRedis.CompanyResponse(HttpStatusCode.OK);

                var httpResponse = await _httpUtil.GetAsync(HostBase.BrazilApi, $"/api/cnpj/v1/{cnpj}");

                var company = await JsonSerializer.DeserializeAsync<Company>(
                await httpResponse.Content.ReadAsStreamAsync(),
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }, cancellationToken);

                await _companyRepository.SetCompanyAsync(company!, cancellationToken);

                return company!.CompanyResponse(httpResponse.StatusCode);
            }
            catch (Exception ex)
            {
                _logger.LogError("[CompanyService][GetCompanyAsync][Exception]: {ex}", ex.Message);
                return ex.Message.CompanyException();
            }
        }
    }
}
