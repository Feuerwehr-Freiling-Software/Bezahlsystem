using IdentityModel.Client;

namespace Blazor_Client.Services
{
    public interface ITokenService
    {
        Task<TokenResponse> GetToken(string scope);
    }
}
