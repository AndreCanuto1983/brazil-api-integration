using Brazil.Api.Integration.Interfaces;
using Brazil.Api.Integration.Models.Base;
using System.Text.Json.Serialization;

namespace Brazil.Api.Integration.Models.BookService
{
    public class BookResponse : IResponseBase
    {
        public Book? Book { get; set; }

        [JsonIgnore]
        public int? StatusCode { get; set; }

        [JsonIgnore]
        public MessageResponse? Message { get; set; }
    }
}
