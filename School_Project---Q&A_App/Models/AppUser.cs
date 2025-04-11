using Microsoft.AspNetCore.Identity;

namespace School_Project___Q_A_App.Models
{
    public class AppUser : IdentityUser
    {
        public string Password { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
        public bool Is_Active { get; set; }
    }
}
