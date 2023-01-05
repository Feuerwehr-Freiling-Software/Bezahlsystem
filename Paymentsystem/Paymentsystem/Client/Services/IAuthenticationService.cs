namespace Paymentsystem.Client.Services
{
    public interface IAuthenticationService
    {
        Task<string> RefreshToken();
    }
}
