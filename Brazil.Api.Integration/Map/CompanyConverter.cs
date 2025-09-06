using Brazil.Api.Integration.Models.Base;
using Brazil.Api.Integration.Models.CompanyService;
using System.Net;

namespace Brazil.Api.Integration.Map
{
    public static class CompanyConverter
    {
        public static CompanyResponse CompanyResponse(this Company company, HttpStatusCode httpStatusCode)
            => new()
            {
                StatusCode = (int)httpStatusCode >= (int)HttpStatusCode.InternalServerError ? (int)HttpStatusCode.BadGateway : (int)httpStatusCode,
                Company = company,
                Message = (int)httpStatusCode >= (int)HttpStatusCode.MultipleChoices ? new MessageResponse
                {
                    Code = (int)httpStatusCode,
                    Description = company.Message
                } : null
            };
    }
}
