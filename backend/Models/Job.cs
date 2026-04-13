using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Karigaar360.Models
{
    public class Job
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Title { get; set; } = string.Empty;

        [Required]
        public string Description { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string Category { get; set; } = string.Empty;

        [Required]
        [Range(1, 1000)]
        public int EstimatedHours { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal FairPrice { get; set; }

        [Required]
        [Range(1, 10000000, ErrorMessage = "Please enter a valid offered price.")]
        [Column(TypeName = "decimal(10,2)")]
        public decimal OfferedPrice { get; set; }

        [Required]
        public string Location { get; set; } = string.Empty;

        public int CustomerId { get; set; }

        [ForeignKey("CustomerId")]
        public Customer? Customer { get; set; }

        [StringLength(100)]
        public string CustomerName { get; set; } = string.Empty;

        public int? WorkerId { get; set; }

        [ForeignKey("WorkerId")]
        public Worker? Worker { get; set; }

        [StringLength(100)]
        public string? WorkerName { get; set; }

        [StringLength(20)]
        public string Status { get; set; } = "Open"; // Open, Accepted, Completed

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? CompletedAt { get; set; }
    }
}
