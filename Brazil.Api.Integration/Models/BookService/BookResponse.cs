using System.Text.Json.Serialization;

namespace Brazil.Api.Integration.Models.BookService
{
    public class BookResponse
    {
        [JsonIgnore]
        public bool Success { get; set; }

        public string? Message { get; set; }

        public Book? Book { get; set; }
    }
}
