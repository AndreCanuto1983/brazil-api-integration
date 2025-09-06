using System.Text.Json.Serialization;

namespace Brazil.Api.Integration.Models.BookService
{
    public class Book
    {
        public string Isbn { get; set; }

        public string Title { get; set; }

        public string SubTitle { get; set; }

        public string Publisher { get; set; }

        public string Synopsis { get; set; }

        public int? Year { get; set; }

        [JsonPropertyName("page_count")]
        public int? PageCount { get; set; }

        public string Location { get; set; }

        [JsonPropertyName("cover_url")]
        public string CoverUrl { get; set; }

        public List<string> Authors { get; set; }

        public List<string> Subjects { get; set; }

        public string Message { get; set; }
    }
}