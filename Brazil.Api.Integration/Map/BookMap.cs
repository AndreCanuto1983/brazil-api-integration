using Brazil.Api.Integration.Models.Base;
using Brazil.Api.Integration.Models.BookService;
using System.Net;

namespace Brazil.Api.Integration.Map
{
    public static class BookMap
    {
        public static BookResponse BookResponse(this Book book, HttpStatusCode httpStatusCode)
            => new()
            {
                StatusCode = (int)httpStatusCode >= (int)HttpStatusCode.InternalServerError ? (int)HttpStatusCode.BadGateway : (int)httpStatusCode,
                Book = book,
                Message = (int)httpStatusCode >= (int)HttpStatusCode.MultipleChoices ? new MessageResponse
                {
                    Code = (int)httpStatusCode,
                    Description = book.Message
                } : null
            };
    }
}
