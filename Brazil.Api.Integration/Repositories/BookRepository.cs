using Brazil.Api.Integration.Interfaces;
using Brazil.Api.Integration.Models.BookService;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace Brazil.Api.Integration.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly IDistributedCache _distributedCache;
        private readonly ILogger<BookRepository> _logger;

        public BookRepository(
            IDistributedCache distributedCache,
            ILogger<BookRepository> logger
            )
        {
            _distributedCache = distributedCache;
            _logger = logger;
        }

        public async Task SetBookAsync(Book? book, CancellationToken cancellationToken)
        {
            try
            {
                if (book is null)
                    return;

                await _distributedCache.SetStringAsync(
                    book.Isbn,
                    JsonSerializer.Serialize(book),
                    cancellationToken);
            }
            catch (Exception ex)
            {
                _logger.LogError("[BookRepository][SetBookAsync] => EXCEPTION: {ex}", ex.Message);
            }
        }

        public async Task<Book?> GetBookAsync(string key, CancellationToken cancellationToken)
        {
            try
            {
                var response = await _distributedCache.GetAsync(key, cancellationToken);

                if (response == null)
                    return null;

                return JsonSerializer.Deserialize<Book>(response);
            }
            catch (Exception ex)
            {
                _logger.LogError("[BookRepository][GetBookAsync] => EXCEPTION: {ex}", ex.Message);
                return null;
            }
        }
    }
}
