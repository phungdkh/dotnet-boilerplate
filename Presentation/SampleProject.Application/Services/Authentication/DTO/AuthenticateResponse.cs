using Newtonsoft.Json;

namespace SampleProject.Application.Services.Authentication.DTO
{
    public record AuthenticateResponse([property: JsonProperty("access_token")] string AccessToken, [property: JsonProperty("refresh_token")] string RefreshToken);
}
