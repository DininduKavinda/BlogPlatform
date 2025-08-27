using System;
using System.ComponentModel.DataAnnotations;

namespace TaskManagement.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string CategoryName { get; set; }

        public ICollection<PostCategory> PostCategories { get; set; } = new List<PostCategory>();
    }
}