using Microsoft.EntityFrameworkCore;
using RealTimeThingSpeak.Models;

namespace RealTimeThingSpeak.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Feed> Feeds { get; set; }
    }
}
