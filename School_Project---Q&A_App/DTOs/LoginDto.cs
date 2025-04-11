using System.ComponentModel.DataAnnotations;

namespace School_Project___Q_A_App.DTOs
{
    public class LoginDto
    {
        [Display(Name = "UserName")]
        [Required(ErrorMessage = "Please enter an Username!")]
        public string UserName { get; set; }

        [Display(Name = "Password")]
        [Required(ErrorMessage = "Please enter a Password!")]
        public string Password { get; set; }

    }
}
