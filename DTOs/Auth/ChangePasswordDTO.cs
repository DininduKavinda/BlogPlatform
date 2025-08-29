using System.ComponentModel.DataAnnotations;

namespace TaskManagement.DTOs.Auth
{
    public class ChangePasswordDTO
    {
        [Required]
        [StringLength(100, MinimumLength = 8)]
        public string Password { get; set; }

        [Compare("Password")]
        public string ConfirmPassword { get; set; }
    }
}