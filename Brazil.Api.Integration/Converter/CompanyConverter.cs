using Brazil.Api.Integration.Models.Base;
using Brazil.Api.Integration.Models.CompanyService;
using System.Net;

namespace Brazil.Api.Integration.Converter
{
    public static class CompanyConverter
    {
        public static CompanyResponse CompanyResponse(this Company company, HttpStatusCode httpStatusCode)
            => new()
            {
                StatusCode = (int)httpStatusCode >= 500 ? (int)HttpStatusCode.BadGateway : (int)httpStatusCode,
                Company = company,
                Message = (int)httpStatusCode >= 299 ? new MessageResponse
                {
                    Code = (int)httpStatusCode,
                    Description = company.Message
                } : null
            };

        public static CompanyResponse CompanyException(this string exception)
            => new()
            {
                StatusCode = (int)HttpStatusCode.InternalServerError,
                Message = new MessageResponse()
                {
                    Code = (int)HttpStatusCode.InternalServerError,
                    Description = exception
                }
            };
    }
}
