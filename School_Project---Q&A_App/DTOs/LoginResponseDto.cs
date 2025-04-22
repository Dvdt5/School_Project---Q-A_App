using System.ComponentModel.DataAnnotations;
namespace School_Project___Q_A_App.DTOs
{
    public class LoginResponseDto
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public string Token { get; set; }
    }
}
