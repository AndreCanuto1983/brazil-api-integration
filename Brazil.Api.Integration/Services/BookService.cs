using Brazil.Api.Integration.Converter;
using Brazil.Api.Integration.Enums;
using Brazil.Api.Integration.Interfaces;
using Brazil.Api.Integration.Models;
using Brazil.Api.Integration.Models.BookService;
using Microsoft.OpenApi.Extensions;
using System.Text.Json;

namespace Brazil.Api.Integration.Services
{
    public class BookService : IBookService
    {        
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

                var client = _httpClientFactory.CreateClient(Hosts.BrazilApi.GetDisplayName());

                var response = await client.GetAsync($"/api/isbn/v1/{isbn}");

                _logger.LogInformation("[BookService][GetBookAsync] => STATUS CODE: {statusCode}, RESPONSE: {response}", 
                    (int)response.StatusCode, await response.Content.ReadAsStringAsync());

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
