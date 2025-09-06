using Brazil.Api.Integration.Configurations;
using Brazil.Api.Integration.Enums;
using Brazil.Api.Integration.Interfaces;
using Brazil.Api.Integration.Map;
using Brazil.Api.Integration.Models.BookService;
using System.Net;
using System.Text.Json;

namespace Brazil.Api.Integration.Services
{
    public class BookService(
        ILogger<BookService> logger,
        IHttpUtil httpUtil,
        IBookRepository bookRepository) : IBookService
    {
        private readonly ILogger<BookService> _logger = logger;
        private readonly IHttpUtil _httpUtil = httpUtil;
        private readonly IBookRepository _bookRepository = bookRepository;

        public async Task<BookResponse> GetBookAsync(string isbn, CancellationToken cancellationToken)
        {
            try
            {
                var bookInRedis = await _bookRepository.GetBookAsync(isbn, cancellationToken);

                if (bookInRedis?.Isbn is not null)
                    return bookInRedis.BookResponse(HttpStatusCode.OK);

                var httpResponse = await _httpUtil.ExecuteAsync(
                    string.Empty,
                    HostBase.BrazilApi,
                    $"/api/isbn/v1/{isbn}",
                    HttpMethod.Get);

                var book = await JsonSerializer.DeserializeAsync<Book>(
                    await httpResponse.Content.ReadAsStreamAsync(cancellationToken),
                        JsonExtension.JsonOptions(),
                        cancellationToken);

                if (httpResponse.IsSuccessStatusCode)
                    if (httpResponse.IsSuccessStatusCode)
                        _ = Task.Run(() => _bookRepository.SetBookAsync(book, cancellationToken));

                return book!.BookResponse(
                    (int)httpResponse.StatusCode == (int)HttpStatusCode.BadRequest ?
                    HttpStatusCode.NoContent :
                    httpResponse.StatusCode);
            }
            catch (Exception ex)
            {
                _logger.LogError("[GetBookAsync][Exception]: {ex}", ex.Message);
                return ex.MapToErrorResponse<BookResponse>();
            }
        }
    }
}
