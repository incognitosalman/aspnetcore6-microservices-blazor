using Client.Blazor.Models;
using IdentityModel.Client;
using Microsoft.Extensions.Options;

namespace Client.Blazor.Services
{
    public class TokenService : ITokenService
    {
        private readonly IOptions<IdentityServerSettings> _identityServerSettings;
        private readonly DiscoveryDocumentResponse _discoveryDocumentResponse;
        private readonly HttpClient _httpClient;

        public TokenService(IOptions<IdentityServerSettings> identityServerSettings)
        {
            _identityServerSettings = identityServerSettings;
            _httpClient = new HttpClient();
            _discoveryDocumentResponse = _httpClient.GetDiscoveryDocumentAsync(
                _identityServerSettings.Value.DiscoveryUrl).Result;

            if (_discoveryDocumentResponse.IsError)
            {
                throw new Exception("Unable to get discovery document",
                    _discoveryDocumentResponse.Exception);
            }
        }

        public async Task<TokenResponse> GetTokenAsync(string scope)
        {
            var tokenResponse = await _httpClient.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
            {
                Address = _discoveryDocumentResponse.TokenEndpoint,
                ClientId = _identityServerSettings.Value.ClientName,
                ClientSecret = _identityServerSettings.Value.ClientPassword,
                Scope = scope
            });

            if (tokenResponse.IsError)
            {
                throw new Exception("Unable to get a token", tokenResponse.Exception);
            }

            return tokenResponse;
        }
    }
}
