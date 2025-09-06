using Brazil.Api.Integration.Configurations;
using Brazil.Api.Integration.Models.Base;
using System.Text.Json;

namespace Brazil.Api.Integration.Map
{
    public static class ErrorMap
    {
        public static async Task<MessageResponse> ExtractErrorResponseAsync(this HttpResponseMessage httpResponse, CancellationToken cancellationToken)
        {
            var error = new MessageResponse();

            if (!httpResponse.IsSuccessStatusCode)
            {
                try
                {
                    var result = await JsonSerializer.DeserializeAsync<MessageResponse>(
                        await httpResponse.Content.ReadAsStreamAsync(cancellationToken),
                        JsonExtension.JsonOptions(),
                        cancellationToken);

                    error.Code = result.Code == 0 || result?.Code is null ? (int)httpResponse.StatusCode : result.Code;
                    error.Description = result?.Description ?? httpResponse.ReasonPhrase;
                }
                catch (Exception)
                {
                    error.Code = (int)httpResponse.StatusCode;
                }
            }

            return error;
        }
    }
}
