using Brazil.Api.Integration.Interfaces;
using Brazil.Api.Integration.Models.Base;
using Brazil.Api.Integration.Models.BookService;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;

namespace Brazil.Api.Integration.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly IBookService _bookService;
        public BooksController(IBookService bookService)
        {
            _bookService = bookService;
        }

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
        [HttpGet("{isbn}")]
        [ProducesResponseType(typeof(Book), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(MessageError), StatusCodes.Status400BadRequest)]        
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]        
        [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
        public async Task<IActionResult> GetBook(string isbn, CancellationToken cancellationToken)
        {
            if (!Regex.IsMatch(isbn, @"^[0-9]+$"))
                return BadRequest("Please enter numbers only");

            var response = await _bookService.GetBookAsync(isbn, cancellationToken);

            if (response.Success)
                return Ok(response);

            return NoContent();            
        }
    }
}