using Microsoft.EntityFrameworkCore;
using KibabiiRevisionGroup.Models;

namespace KibabiiRevisionGroup.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Student> Students { get; set; }
        public DbSet<AssessmentResult> AssessmentResults { get; set; }
    }
}