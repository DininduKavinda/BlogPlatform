using TaskManagement.DTOs.Auth;
using TaskManagement.Models;

namespace TaskManagement.Services.Interfaces
{
    public interface IAuthService
    {
        Task<bool> RegisterAsync(RegisterDTO registerDTO);
        Task<User> LoginAsync(LoginDTO loginDTO);
        Task<string> GeneratePasswordResetTokenAsync(string email);
        Task<bool> ResetPasswordAsync(string email, string token, string newPassword);
        Task<bool> ValidateResetTokenAsync(string email, string token);
    }
}