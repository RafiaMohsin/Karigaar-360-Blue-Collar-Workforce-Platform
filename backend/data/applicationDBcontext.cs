using Microsoft.EntityFrameworkCore;
using Karigaar360.Models;

namespace Karigaar360.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        
        public DbSet<Worker> Workers { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Job> Jobs { get; set; }
        public DbSet<JobApplication> JobApplications { get; set; }
        public DbSet<Rating> Ratings { get; set; }
        public DbSet<ChatMessage> ChatMessages { get; set; }
    }
}