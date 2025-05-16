using System.ComponentModel.DataAnnotations;

namespace ABSOLUTE_CINEMA.Models
{
    public class Register
    {
        [Required(ErrorMessage = "Имя обязательно для заполнения")]
        [Display(Name = "Имя")]
        public string Name { get; set; }
        
        [Required(ErrorMessage = "Email обязателен для заполнения")]
        [EmailAddress(ErrorMessage = "Некорректный email")]
        [Display(Name = "Email")]
        public string Email { get; set; }
        
        [Required(ErrorMessage = "Пароль обязателен для заполнения")]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        [StringLength(100, ErrorMessage = "Пароль должен содержать минимум 6 символов", MinimumLength = 6)]
        public string Password { get; set; }
        
        [DataType(DataType.Password)]
        [Display(Name = "Подтверждение пароля")]
        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        public string ConfirmPassword { get; set; }
        
        
        [Display(Name = "Согласен с условиями")]
        [Required(ErrorMessage = "Необходимо согласие с условиями")]
        public bool Terms { get; set; } = false;
        
        
    }
}