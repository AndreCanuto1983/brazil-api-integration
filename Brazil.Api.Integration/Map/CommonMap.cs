using Brazil.Api.Integration.Interfaces;
using Brazil.Api.Integration.Models.Base;
using System.Net;

namespace Brazil.Api.Integration.Map
{
    public static class CommonMap
    {
        public static T MapToErrorResponse<T>(this Exception exception, MessageResponse messageResponse = null)
            where T : IResponseBase, new()
            => new()
            {
                StatusCode = (int)HttpStatusCode.BadGateway,
                Message = new MessageResponse()
                {
                    Code = messageResponse?.Code is null || messageResponse.Code == 0 ?
                    (int)HttpStatusCode.BadGateway :
                    messageResponse.Code,

                    Description = string.IsNullOrEmpty(messageResponse?.Description) ?
                    exception.Message :
                    messageResponse.Description
                }
            };
    }
}
