using AuthenticationApi.Models;

namespace AuthenticationApi.Services
{
    public interface IAuthenticationService
    {
        Task<bool> Login(LoginModel model);
        Task<bool> RegisterUser(RegisterModel model);
    }
}
