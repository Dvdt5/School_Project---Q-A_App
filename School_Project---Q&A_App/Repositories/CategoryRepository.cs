using School_Project___Q_A_App.Models;

namespace School_Project___Q_A_App.Repositories
{
    public class CategoryRepository : GenericRepository<Category>
    {
        public CategoryRepository(AppDbContext context) : base(context)
        {
        }
    }
}
