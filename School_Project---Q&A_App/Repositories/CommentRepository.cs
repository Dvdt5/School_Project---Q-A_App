using School_Project___Q_A_App.Models;

namespace School_Project___Q_A_App.Repositories
{
    public class CommentRepository : GenericRepository<Comment>
    {
        public CommentRepository(AppDbContext context) : base(context)
        {
        }
    }
}
