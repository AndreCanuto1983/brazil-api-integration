using Brazil.Api.Integration.Converter;
using Brazil.Api.Integration.Enums;
using Brazil.Api.Integration.Interfaces;
using Brazil.Api.Integration.Models;
using Brazil.Api.Integration.Models.BookService;
using System.Text.Json;

namespace Brazil.Api.Integration.Services
{
    public class BookService : IBookService
    {        
        private readonly ILogger<BookService> _logger;
        private readonly IHttpUtil _httpUtil;
        private readonly IBookRepository _bookRepository;

        public BookService(
            ILogger<BookService> logger,
            IHttpUtil httpUtil,
            IBookRepository bookRepository)
        {
            _logger = logger;
            _httpUtil = httpUtil;
            _bookRepository = bookRepository;
        }

        public async Task<BookResponse> GetBookAsync(string isbn, CancellationToken cancellationToken)
        {
            try
            {
                var bookInRedis = await _bookRepository.GetBookAsync(isbn, cancellationToken);

                if (bookInRedis is not null)
                    return bookInRedis.Success();

                var httpResponse = await _httpUtil.GetAsync(HostBase.BrazilApi, $"/api/isbn/v1/{isbn}");

                if (httpResponse.IsSuccessStatusCode)
                {
                    var book = await JsonSerializer.DeserializeAsync<Book>(
                    await httpResponse.Content.ReadAsStreamAsync(),
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    }, cancellationToken);

                    await _bookRepository.SetBookAsync(book, cancellationToken);

                    return book!.Success();
                }

                var error = await JsonSerializer.DeserializeAsync<MessageError>(
                        await httpResponse.Content.ReadAsStreamAsync(),
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
