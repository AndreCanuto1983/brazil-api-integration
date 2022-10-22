using Brazil.Api.Integration.Models.BookService;

namespace Brazil.Api.Integration.Interfaces
{
    public interface IBookService
    {
        Task<BookResponse> GetBookAsync(string isbn, CancellationToken cancellationToken);
    }
}
