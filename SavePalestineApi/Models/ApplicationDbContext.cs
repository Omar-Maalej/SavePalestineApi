using Microsoft.EntityFrameworkCore;

namespace SavePalestineApi.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; } 
        public DbSet<Fundraising> Fundraisings { get; set; }
    }
}
