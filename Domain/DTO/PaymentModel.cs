﻿namespace ABSOLUTE_CINEMA.Domain.DTO
{
    public class PaymentModel
    {
        public string CardNumber { get; set; }
        public string ExpiryDate { get; set; }
        public string CVV { get; set; }
        public string SubscriptionType { get; set; }
    }
}