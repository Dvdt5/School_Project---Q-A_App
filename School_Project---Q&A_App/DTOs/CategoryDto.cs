using System.ComponentModel.DataAnnotations;

namespace School_Project___Q_A_App.DTOs
{
    public class CategoryDto : BaseDto
    {
        [Required(ErrorMessage = "Category Name is required!")]
        public string Name { get; set; }
    }
}
