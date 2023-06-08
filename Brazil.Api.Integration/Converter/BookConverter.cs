using Brazil.Api.Integration.Models.Base;
using Brazil.Api.Integration.Models.BookService;

namespace Brazil.Api.Integration.Converter
{
    public static class BookConverter
    {
        public static BookResponse Success(this Book book)
            => new()
            {
                Success = true,
                Book = book
            };

        public static BookResponse BookUnsuccessfully(this MessageError messageError)
            => new()
            {
                Success = false,
                Message = messageError.Message
            };
    }
}
