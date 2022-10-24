using Brazil.Api.Integration.Interfaces;
using Brazil.Api.Integration.Models;
using Brazil.Api.Integration.Models.CompanyService;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;

namespace Brazil.Api.Integration.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyService _companyService;

        public CompanyController(ICompanyService companyService)
        {
            _companyService = companyService;
        }

        [HttpGet("{cnpj}")]
        [ProducesResponseType(typeof(Company), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(MessageError), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
        public async Task<IActionResult> GetCompany(string cnpj, CancellationToken cancellationToken)
        {
            if (!Regex.IsMatch(cnpj, @"^[0-9]+$"))
                return BadRequest("Please enter numbers only");

            if (cnpj.Length < 14 || cnpj.Length > 14)
                return BadRequest("Please enter a Cnpj with 14 digits");

            var response = await _companyService.GetCompanyAsync(cnpj, cancellationToken);

            if (!response.Success)
                return NoContent();

            return Ok(response);
        }
    }
}
