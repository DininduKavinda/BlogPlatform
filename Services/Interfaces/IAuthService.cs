using TaskManagement.DTOs.Auth;
using TaskManagement.Services.Helpers;

namespace TaskManagement.Services.Interfaces
{
    public interface IAuthService 
    {
        Task<AuthenticationResult> RegisterAsync(RegisterDTO registerDTO);
        Task<AuthenticationResult> LoginAsync(LoginDTO loginDTO);
        Task<bool> LogoutAsync();
        Task<bool> ChangePasswordAsync(int UserId, ChangePasswordDTO changePasswordDTO);
    }
}