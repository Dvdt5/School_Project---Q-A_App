using School_Project___Q_A_App.Models;
using System.ComponentModel.DataAnnotations;

namespace School_Project___Q_A_App.DTOs
{
    public class CommentDto
    {
        public int Id { get; set; }
        public int PostId { get; set; }
        public string UserId { get; set; }

        [Display(Name = "Content")]
        [Required(ErrorMessage = "Please enter a Content!")]
        public string Content { get; set; }

        public AppUser User { get; set; }
        public Post Post { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
    }
}
