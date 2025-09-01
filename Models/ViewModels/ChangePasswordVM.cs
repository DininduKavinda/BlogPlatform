using System.ComponentModel.DataAnnotations;

namespace TaskManagement.Models.ViewModels
{
    public class ChangePasswordVM
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [MinLength(8)]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Compare("NewPassword")]
        public string ConfirmPassword { get; set; }

        public string Token { get; set; }
    }
}