using System.ComponentModel.DataAnnotations;

namespace Cinema.Core.DTO
{
    public class LoginDTO
    {
        [Required(ErrorMessage = "Email Address can't be blank")]
        [EmailAddress(ErrorMessage = "Email should be in proper email address format")]
        public string EmailOrUsername { get; set; } = string.Empty;

        [Required(ErrorMessage = "Password can't be blank")]
        public string Password { get; set; } = string.Empty;
    }
}
