using SavePalestineApi.Models;

namespace SavePalestineApi.Services
{
    public interface IAuthService
    {
        string GenerateTokenString(LoginUser user);
        Task<bool> Login(LoginUser user);
        Task<bool> RegisterUser(LoginUser user, string role);
    }
}
