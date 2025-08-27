using System;
using System.ComponentModel.DataAnnotations;

namespace TaskManagement.Models
{
    public class Role
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string RoleName { get; set; }

        public ICollection<User> Users { get; set; } = new List<User>();
        
    }
}