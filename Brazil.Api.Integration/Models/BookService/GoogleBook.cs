namespace Brazil.Api.Integration.Models.BookService
{
    public class GoogleBookItems
    {
        public List<GoogleBook>? Items { get; set; }
    }

    public class GoogleBook
    {
        public string Id { get; set; }
        public VolumeInfo VolumeInfo { get; set; }
    }

    public class VolumeInfo
    {
        public string Title { get; set; }
        public List<string> Authors { get; set; }
        public string Publisher { get; set; }
        public string PublishedDate { get; set; }
        public string Description { get; set; }        
        public int PageCount { get; set; }
        public List<string> Categories { get; set; }
        public ImageLinks ImageLinks { get; set; }
        public string Language { get; set; }
        public List<IndustryIdentifiers> IndustryIdentifiers { get; set; }
    }

    public class ImageLinks
    {
        public string SmallThumbnail { get; set; }
        public string Thumbnail { get; set; }
    }

    public class IndustryIdentifiers
    {
        public string Type { get; set; }
        public string Identifier { get; set; }
    }
}
