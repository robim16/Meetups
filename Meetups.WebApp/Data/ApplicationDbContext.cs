using Microsoft.EntityFrameworkCore;

namespace Meetups.WebApp.Data
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }
        public DbSet<Entities.Event> Events { get; set; }
    }
}
