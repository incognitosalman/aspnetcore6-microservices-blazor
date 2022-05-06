using IdentityModel.Client;

namespace Client.Blazor.Services
{
    public interface ITokenService
    {
        Task<TokenResponse> GetTokenAsync(string scope);
    }
}
