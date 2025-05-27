using System.ComponentModel.DataAnnotations;

namespace ABSOLUTE_CINEMA.AbsoluteCinema.ViewModels
{
    public class PaymentViewModel
    {
        [Required, CreditCard]
        [Display(Name = "Номер карты")]
        public string CardNumber { get; set; }

        [Required]
        [RegularExpression(@"\d{2}/\d{2}", ErrorMessage = "Введите срок в формате MM/YY")]
        [Display(Name = "Срок действия (MM/YY)")]
        public string ExpiryDate { get; set; }

        [Required, StringLength(4, MinimumLength = 3)]
        [Display(Name = "CVV")]
        public string CVV { get; set; }

        [Required]
        [Display(Name = "Тип подписки")]
        public string SubscriptionType { get; set; }
    }
}
