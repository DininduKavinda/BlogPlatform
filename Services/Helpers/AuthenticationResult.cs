using TaskManagement.DTOs;

namespace TaskManagement.Services.Helpers
{
    public class AuthenticationResult
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public string Token { get; set; }
        public UserDTO User { get; set; }
    }
} 