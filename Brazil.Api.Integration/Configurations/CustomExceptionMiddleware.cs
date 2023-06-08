using Brazil.Api.Integration.Models.Base;
using System.Net;
using System.Text.Json;

namespace Brazil.Api.Integration.Configurations
{
    public class CustomExceptionMiddleware
    {
        private readonly RequestDelegate _requestDelegate;
        public CustomExceptionMiddleware(RequestDelegate requestDelegate)
        {
            _requestDelegate = requestDelegate;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _requestDelegate(httpContext);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex).ConfigureAwait(false);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
                        
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var result = new MessageResponse
            {
                Code = (int)HttpStatusCode.InternalServerError,
                Description = exception.Message
            };

            return context.Response.WriteAsync(JsonSerializer.Serialize(result));
        }
    }
}
