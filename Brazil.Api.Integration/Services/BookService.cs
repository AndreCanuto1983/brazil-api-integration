using Brazil.Api.Integration.Converter;
using Brazil.Api.Integration.Interfaces;
using Brazil.Api.Integration.Models;
using Brazil.Api.Integration.Models.BookService;
using System.Text.Json;

namespace Brazil.Api.Integration.Services
{
    public class BookService : IBookService
    {
        private const string PATH_NAME = "/api/isbn/v1";
        private readonly ILogger<BookService> _logger;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IBookRepository _bookRepository;

        public BookService(
            ILogger<BookService> logger,
            IHttpClientFactory httpClientFactory,
            IBookRepository bookRepository)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;
            _bookRepository = bookRepository;
        }

        public async Task<BookResponse> GetBookAsync(string isbn, CancellationToken cancellationToken)
        {
            try
            {
                var bookInRedis = await _bookRepository.GetBookAsync(isbn, cancellationToken);

                if (bookInRedis is not null)
                    return bookInRedis.Success();

                var client = _httpClientFactory.CreateClient("BrazilApi");

                var response = await client.GetAsync($"{PATH_NAME}/{isbn}");

                if (response.IsSuccessStatusCode)
                {
                    var book = await JsonSerializer.DeserializeAsync<Book>(
                    await response.Content.ReadAsStreamAsync(),
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    }, cancellationToken);

                    await _bookRepository.SetBookAsync(book, cancellationToken);

                    return book!.Success();
                }

                var error = await JsonSerializer.DeserializeAsync<MessageError>(
                        await response.Content.ReadAsStreamAsync(),
                        new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true
                        }, cancellationToken);

                _logger.LogWarning("[BookService][GetBookAsync][Response]: {response}",
                await response.Content.ReadAsStringAsync());

                return error!.BookUnsuccessfully();
            }
            catch (Exception ex)
            {
                _logger.LogError("[BookService][GetBookAsync][Exception]: {ex}", ex.Message);
                throw;
            }
        }
    }
}
