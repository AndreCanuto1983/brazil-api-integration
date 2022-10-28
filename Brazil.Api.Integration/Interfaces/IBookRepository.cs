using Brazil.Api.Integration.Models.BookService;

namespace Brazil.Api.Integration.Interfaces
{
    public interface IBookRepository
    {
        Task SetBookAsync(Book? book, CancellationToken cancellationToken);
        Task<Book?> GetBookAsync(string key, CancellationToken cancellationToken);
    }
}
