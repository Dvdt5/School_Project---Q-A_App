﻿using System.ComponentModel.DataAnnotations;

namespace School_Project___Q_A_App.DTOs
{
    public class UserDto : BaseDto
    {
        [Display(Name = "UserName")]
        [Required(ErrorMessage = "Please enter an Username!")]
        public string UserName { get; set; }

        [Display(Name = "Email")]
        [Required(ErrorMessage = "Please enter an Email!")]
        public string Email { get; set; }

        public string Password { get; set; }

    }
}
