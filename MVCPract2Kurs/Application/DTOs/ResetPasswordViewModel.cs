using System.ComponentModel.DataAnnotations;

namespace Application.DTOs
{
    public class ResetPasswordViewModel
    {
        [Required]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Вопрос подтверждения")]
        public string ResetPasswordQuestion { get; set; }
        [Required]
        [Display(Name = "Ответ на вопрос")]
        public string ResetPasswordAnswer { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Новый пароль")]
        public string NewPassword { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage = "Новый пароль и пароль подтверждения не совпадают.")]
        [Display(Name = "Повторный ввод пароля")]
        public string ConfirmPassword { get; set; }
    }
}
