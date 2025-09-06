using Brazil.Api.Integration.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Brazil.Api.Integration.Common
{
    [ApiController]
    [Produces("application/json")]
    public abstract class BaseController : ControllerBase
    {
        protected IActionResult ProcessResponse(IResponseBase response)
        {
            switch (response.StatusCode)
            {
                case StatusCodes.Status200OK:
                    response.StatusCode = null;
                    return Ok(response);

                case StatusCodes.Status201Created:
                    return Created(string.Empty, string.Empty);

                case StatusCodes.Status204NoContent:
                    return NoContent();

                case StatusCodes.Status400BadRequest:
                    return BadRequest(response?.Message);

                case StatusCodes.Status404NotFound:
                    return NotFound(response?.Message);

                case StatusCodes.Status500InternalServerError:
                    return StatusCode(StatusCodes.Status500InternalServerError, response?.Message);

                case StatusCodes.Status503ServiceUnavailable:
                    return StatusCode(StatusCodes.Status503ServiceUnavailable, response?.Message);

                default:
                    return StatusCode(StatusCodes.Status502BadGateway, response?.Message);
            }
        }
    }
}
