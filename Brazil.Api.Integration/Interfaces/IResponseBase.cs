using Brazil.Api.Integration.Models.Base;

namespace Brazil.Api.Integration.Interfaces
{
    public interface IResponseBase
    {
        public int? StatusCode { get; set; }
        public MessageResponse? Message { get; set; }
    }
}
