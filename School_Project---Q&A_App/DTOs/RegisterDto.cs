using System.ComponentModel.DataAnnotations;

namespace School_Project___Q_A_App.DTOs
{
    public class RegisterDto
    {
        [Display(Name = "UserName")]
        [Required(ErrorMessage = "Please enter an Username!")]
        public string UserName { get; set; }

        [Display(Name = "Email")]
        [Required(ErrorMessage = "Please enter an Email!")]
        public string Email { get; set; }

        [Display(Name = "Password")]
        [Required(ErrorMessage = "Please enter a Password!")]
        public string Password { get; set; }

        [Display(Name = "Password Confirm")]
        [Required(ErrorMessage = "Please confrim Password!")]
        [Compare("Password", ErrorMessage = "Password doesn't match!")]
        public string PasswordConfirm { get; set; }

        public bool Is_Active { get; set; } = true;
    }
}
