using System.ComponentModel.DataAnnotations;

namespace Application.DTOs
{
    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

    }
}
