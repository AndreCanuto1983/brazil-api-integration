using Brazil.Api.Integration.Models.Base;
using Brazil.Api.Integration.Models.BookService;
using System.Net;

namespace Brazil.Api.Integration.Converter
{
    public static class BookMap
    {
        public static BookResponse BookResponse(this Book book, HttpStatusCode httpStatusCode)
            => new()
            {
                StatusCode = (int)httpStatusCode >= 500 ? (int)HttpStatusCode.BadGateway : (int)httpStatusCode,
                Book = book,
                Message = (int)httpStatusCode >= 299 ? new MessageResponse
                {
                    Code = (int)httpStatusCode,
                    Description = book.Message
                } : null
            };

        public static BookResponse BookException(this string exception)
            => new()
            {
                StatusCode = (int)HttpStatusCode.InternalServerError,
                Message = new MessageResponse()
                {
                    Code = (int)HttpStatusCode.InternalServerError,
                    Description = exception
                }
            };
    }
}
