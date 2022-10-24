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

        public BookService(
            ILogger<BookService> logger,
            IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<BookResponse> GetBookAsync(string isbn, CancellationToken cancellationToken)
        {
            try
            {
                var client = _httpClientFactory.CreateClient("BrazilApi");

                var response = await client.GetAsync($"{PATH_NAME}/{isbn}");

                if (response.IsSuccessStatusCode)
                {
                    var result = await JsonSerializer.DeserializeAsync<Book>(
                    await response.Content.ReadAsStreamAsync(),
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    }, cancellationToken);

                    return result!.Success();
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
