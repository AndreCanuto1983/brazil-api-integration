using Brazil.Api.Integration.Models.Base;
using System.Text.Json;

namespace Brazil.Api.Integration.Configurations
{
    public class CustomExceptionMiddleware(RequestDelegate requestDelegate)
    {
        private readonly RequestDelegate _requestDelegate = requestDelegate;

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

            var result = new MessageResponse
            {
                Code = context.Response.StatusCode,
                Description = exception.Message
            };

            return context.Response.WriteAsync(JsonSerializer.Serialize(result));
        }
    }
}
