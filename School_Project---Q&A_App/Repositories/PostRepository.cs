using School_Project___Q_A_App.Models;

namespace School_Project___Q_A_App.Repositories
{
    public class PostRepository : GenericRepository<Post>
    {
        public PostRepository(AppDbContext context) : base(context)
        {
        }
    }
}
