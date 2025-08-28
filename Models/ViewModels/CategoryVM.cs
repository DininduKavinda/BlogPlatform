using System.ComponentModel.DataAnnotations;

namespace TaskManagement.Models.ViewModels
{
    public class CategoryVM
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Category Name")]
        [StringLength(10, ErrorMessage = "Category name cannot exceed 10 characters.")]
        public string CategoryName { get; set; }
    }
}