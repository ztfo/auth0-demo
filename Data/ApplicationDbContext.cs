using Microsoft.EntityFrameworkCore;

namespace auth0_demo
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // Your DbSet properties go here...
    }
}