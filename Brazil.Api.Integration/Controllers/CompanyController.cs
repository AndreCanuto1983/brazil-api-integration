using Brazil.Api.Integration.Common;
using Brazil.Api.Integration.Interfaces;
using Brazil.Api.Integration.Models.Base;
using Brazil.Api.Integration.Models.CompanyService;
using Microsoft.AspNetCore.Mvc;

namespace Brazil.Api.Integration.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class CompanyController :  BaseController
    {
        private readonly ICompanyService _companyService;

        public CompanyController(ICompanyService companyService)
        {
            _companyService = companyService;
        }

        /// <summary>
        /// Search for company data through cnpj
        /// </summary>
        /// <param name="cnpj"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <response code="200">Operation success</response>        
        /// <response code="400">Note the sent parameters, something may be wrong</response>
        /// <response code="401">Requires authentication</response>
        /// <response code="500">Internal service error</response>
        /// <response code="502">Service called internally returned some error</response>
        [HttpGet("{cnpj}")]
        [ProducesResponseType(typeof(Company), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(MessageResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
        public async Task<IActionResult> GetCompany(string cnpj, CancellationToken cancellationToken)
        {
            var cnpjIsValid = cnpj.CpfCnpjIsValid();

            if (!string.IsNullOrEmpty(cnpjIsValid))
                return BadRequest(cnpjIsValid);

            return ProcessResponse(
                await _companyService.GetCompanyAsync(cnpj, cancellationToken));
        }
    }
}
