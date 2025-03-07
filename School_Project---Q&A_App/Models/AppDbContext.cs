using Microsoft.EntityFrameworkCore;

namespace School_Project___Q_A_App.Models
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }


        public AppDbContext(DbContextOptions options) : base(options)
        {
        }
    }
}
