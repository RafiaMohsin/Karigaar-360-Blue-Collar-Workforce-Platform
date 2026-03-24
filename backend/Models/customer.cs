using System.ComponentModel.DataAnnotations;

namespace Karigaar360.Models
{
    public class Customer
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        public string FullName { get; set; } = string.Empty;
        
        [Required]
        public string Phone { get; set; } = string.Empty;
        
        public string? Email { get; set; }
        
        public string? Address { get; set; }
        
        [Required]
        public string PasswordHash { get; set; } = string.Empty;
    }
}