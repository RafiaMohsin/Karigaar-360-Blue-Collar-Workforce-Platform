using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Karigaar360.Models
{
    public class Worker
    {
        [Key]
        public int Id { get; set; }
        
        [Required(ErrorMessage = "Full Name is required")]
        public string FullName { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "Phone number is required")]
        [Phone(ErrorMessage = "Invalid phone format")]
        public string Phone { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "Please choose your profession")]
        public string Profession { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "Experience is required")]
        [Range(0, 50, ErrorMessage = "Experience must be between 0 and 50 years")]
        public int? ExperienceYears { get; set; }
        
        [Required(ErrorMessage = "Location is required")]
        public string Location { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "Password is required")]
        [MinLength(8, ErrorMessage = "Password must be at least 8 characters long")]
        public string PasswordHash { get; set; } = string.Empty;

        [ValidateNever]
        public double Rating { get; set; } = 0.0;

        [ValidateNever]
        public int TotalJobsCompleted { get; set; } = 0;

        [ValidateNever]
        public double TotalEarnings { get; set; } = 0.0;

        [ValidateNever]
        public bool IsAvailable { get; set; } = true;
    }
}