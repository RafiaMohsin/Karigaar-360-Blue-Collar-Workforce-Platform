using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Karigaar360.Models
{
    public class JobApplication
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int JobId { get; set; }
        
        [ForeignKey("JobId")]
        [ValidateNever]
        public Job? Job { get; set; }

        [Required]
        public int WorkerId { get; set; }

        [ForeignKey("WorkerId")]
        [ValidateNever]
        public Worker? Worker { get; set; }

        [StringLength(20, ErrorMessage = "Status cannot exceed 20 characters")]
        public string Status { get; set; } = "Pending"; // Pending, Accepted, Rejected

        public DateTime AppliedAt { get; set; } = DateTime.UtcNow;
    }
}
