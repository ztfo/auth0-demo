using Microsoft.EntityFrameworkCore;
using auth0_demo.Models;

namespace auth0_demo
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<User>? Users {get; set;}

    }
}