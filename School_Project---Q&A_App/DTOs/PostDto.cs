using School_Project___Q_A_App.Models;
using System.ComponentModel.DataAnnotations;

namespace School_Project___Q_A_App.DTOs
{
    public class PostDto : BaseDto
    {
        [Required(ErrorMessage = "Title is required!")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Content is required!")]
        public string Content { get; set; }

        public int AnswerCount { get; set; }
        public string UserId { get; set; }
        public AppUser? User { get; set; }
        public int CategoryId { get; set; }
    }
}
