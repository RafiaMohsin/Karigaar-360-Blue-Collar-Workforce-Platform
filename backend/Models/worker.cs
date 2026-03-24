using System.ComponentModel.DataAnnotations;

namespace Karigaar360.Models
{
    public class Worker
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        public string FullName { get; set; } = string.Empty;
        
        [Required]
        public string Phone { get; set; } = string.Empty;
        
        [Required]
        public string Profession { get; set; } = string.Empty;
        
        [Required]
        public int ExperienceYears { get; set; }
        
        [Required]
        public string Location { get; set; } = string.Empty;
        
        [Required]
        public string PasswordHash { get; set; } = string.Empty;
    }
}