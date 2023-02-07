using Brazil.Api.Integration.Interfaces;
using Brazil.Api.Integration.Models;
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