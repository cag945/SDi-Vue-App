using Microsoft.EntityFrameworkCore;

namespace AspNetCore
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<GPARecord> GPARecords { get; set; }
        
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        { }
    }
}