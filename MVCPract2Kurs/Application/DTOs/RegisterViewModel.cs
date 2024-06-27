using System.ComponentModel.DataAnnotations;

namespace Application.DTOs
{
    public class RegisterViewModel
    {
        [Required]
        [Display(Name = "Имя пользователя")]
        public string FullName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Пароль и пароль подтверждения не совпадают.")]
        [Display(Name = "Повторный ввод пароля")]
        public string ConfirmPassword { get; set; }
        [Required]
        [Display(Name = "Вопрос для подтверждения смены пароля")]
        public string ResetPasswordQuestion { get; set; }
        [Required]
        [Display(Name = "Ответ подтверждения")]
        public string ResetPasswordAnswer { get; set; }
    }
}
