using System.ComponentModel.DataAnnotations;

namespace ABSOLUTE_CINEMA.Models
{
    public class Login
    {
        [Required(ErrorMessage = "Email обязателен для заполнения")]
        [EmailAddress(ErrorMessage = "Некорректный email")]
        [Display(Name = "Email")]
        public string Email { get; set; }
        
        [Required(ErrorMessage = "Пароль обязателен для заполнения")]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }
        
        [Display(Name = "Запомнить меня")]
        public bool RememberMe { get; set; } = false;
        
    }
}