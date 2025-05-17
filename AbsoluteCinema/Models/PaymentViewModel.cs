using System.ComponentModel.DataAnnotations;

namespace ABSOLUTE_CINEMA.Models
{
    public class PaymentViewModel
    {
        [Required(ErrorMessage = "Обязательное поле")]
        [CreditCard(ErrorMessage = "Неверный номер карты")]
        [Display(Name = "Номер карты")]
        public string CardNumber { get; set; }

        [Required(ErrorMessage = "Обязательное поле")]
        [RegularExpression(@"^(0[1-9]|1[0-2])\/?([0-9]{2})$", ErrorMessage = "Неверный формат")]
        
        
        [Display(Name = "Срок действия (MM/YY)")]
        public string ExpiryDate { get; set; }

        [Required(ErrorMessage = "Обязательное поле")]
        [StringLength(3, MinimumLength = 3, ErrorMessage = "Неверный CVV")]
        [Display(Name = "CVV")]
        public string CVV { get; set; }

        [Required(ErrorMessage = "Выберите тип подписки")]
        [Display(Name = "Тип подписки")]
        public string SubscriptionType { get; set; }
    }
}