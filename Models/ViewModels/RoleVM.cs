using System.ComponentModel.DataAnnotations;

namespace TaskManagement.Models.ViewModels
{
    public class RoleVM
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Role Name")]
        [StringLength(10, ErrorMessage = "Role name cannot exceed 10 characters.")]
        public string RoleName { get; set; }
    }
}