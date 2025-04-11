using School_Project___Q_A_App.Models;

namespace School_Project___Q_A_App.Repositories
{
    public class UserRepository : GenericRepository<AppUser>
    {
        public UserRepository(AppDbContext context) : base(context)
        {
        }
    }
}
