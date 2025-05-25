using System.ComponentModel.DataAnnotations;
namespace ABSOLUTE_CINEMA.AbsoluteCinema.ViewModels
{ 
public class RegisterViewModel
{
    [Required(ErrorMessage = "Имя обязательно")]
    public string Name { get; set; }

    [Required(ErrorMessage = "Email обязателен")]
    [EmailAddress(ErrorMessage = "Введите корректный email")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Пароль обязателен")]
    [MinLength(6, ErrorMessage = "Минимум 6 символов")]
    public string Password { get; set; }

    [Required(ErrorMessage = "Подтвердите пароль")]
    [Compare("Password", ErrorMessage = "Пароли не совпадают")]
    public string ConfirmPassword { get; set; }
}
}
