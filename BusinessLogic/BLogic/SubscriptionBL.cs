using System;
using ABSOLUTE_CINEMA.BusinessLogic.Core;
using ABSOLUTE_CINEMA.BusinessLogic.Interfaces;
using ABSOLUTE_CINEMA.Domain.DTO;
using ABSOLUTE_CINEMA.Domain.Entities;

namespace ABSOLUTE_CINEMA.BusinessLogic.BLogic
{
    public class SubscriptionBL : SubscriptionApi, ISubscription
    {
        public bool HasActive()
        {
            var session = new SessionBL();
            var userId = session.GetCurrentUserId();
            return HasActive(userId);
        }

        public void Subscribe(PaymentViewModel dto)
        {
            var session = new SessionBL();
            var userId = session.GetCurrentUserId();
            if (HasActive(userId)) return;

            var subscription = new Subscription
            {
                UserId = userId,
                StartDate = DateTime.UtcNow,
                EndDate = dto.SubscriptionType == "Monthly"
                    ? DateTime.UtcNow.AddMonths(1)
                    : DateTime.UtcNow.AddYears(1),
                Type = dto.SubscriptionType,
                IsActive = true
            };

            AddSubscription(subscription);
        }
    }
}
