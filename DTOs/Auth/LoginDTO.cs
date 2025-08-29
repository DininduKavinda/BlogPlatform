using System.ComponentModel.DataAnnotations;

namespace TaskManagement.DTOs.Auth
{
    public class LoginDTO
    {
        [Required]
        public string EmailOrUsername { get; set; }
        [Required]
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}