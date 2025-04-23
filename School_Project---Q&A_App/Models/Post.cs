using School_Project___Q_A_App.DTOs;

namespace School_Project___Q_A_App.Models
{
    public class Post : BaseEntity
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public int CategoryId { get; set; }
        public int UserId { get; set; }
        public Category Category { get; set; }
        public AppUser User { get; set; }

    }
}
