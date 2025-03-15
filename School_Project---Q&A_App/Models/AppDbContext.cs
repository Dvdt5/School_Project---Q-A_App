using Microsoft.EntityFrameworkCore;

namespace School_Project___Q_A_App.Models
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Category> Categories { get; set; }

        public AppDbContext(DbContextOptions options) : base(options)
        {
        }
    }
}
