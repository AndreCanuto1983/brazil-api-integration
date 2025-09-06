using Brazil.Api.Integration.Common;
using Brazil.Api.Integration.Interfaces;
using Brazil.Api.Integration.Models.Base;
using Brazil.Api.Integration.Models.BookService;
using Microsoft.AspNetCore.Mvc;

namespace Brazil.Api.Integration.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class BooksController(IBookService bookService) : BaseController
    {
        private readonly IBookService _bookService = bookService;

        /// <summary>
        /// Search book data through isbn
        /// </summary>
        /// <param name="isbn"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <response code="200">Operation success</response>        
        /// <response code="400">Note the sent parameters, something may be wrong</response>
        /// <response code="401">Requires authentication</response>
        /// <response code="500">Internal service error</response>
        /// <response code="502">Service called internally returned some error</response>
        /// <response code="503">Service unavailable</response>
        [HttpGet("{isbn}")]
        [ProducesResponseType(typeof(Book), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(MessageResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(MessageResponse), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(MessageResponse), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(MessageResponse), StatusCodes.Status502BadGateway)]
        [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
        public async Task<IActionResult> GetBook(string isbn, CancellationToken cancellationToken)
        {
            var onlyNumbersValidate = isbn.OnlyNumbersIsValid();

            if (!string.IsNullOrEmpty(onlyNumbersValidate))
                return BadRequest(onlyNumbersValidate);

            return ProcessResponse(
                await _bookService.GetBookAsync(isbn, cancellationToken));
        }
    }
}