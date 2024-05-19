using System.ComponentModel.DataAnnotations;

namespace Cinema.Core.DTO
{
    public class ForgotPasswordDTO
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
