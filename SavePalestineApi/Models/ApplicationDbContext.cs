using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace SavePalestineApi.Models
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; } 
        public DbSet<Fundraising> Fundraisings { get; set; }
        public DbSet<Boycott> Boycotts { get; set; }
    }
}
