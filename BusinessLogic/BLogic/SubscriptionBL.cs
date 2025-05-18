using System;
using ABSOLUTE_CINEMA.BusinessLogic.Interfaces;
using ABSOLUTE_CINEMA.BusinessLogic.Core;
using ABSOLUTE_CINEMA.Domain.DTO;
using ABSOLUTE_CINEMA.Domain.Entities;
using BusinessLogic.Core;

namespace ABSOLUTE_CINEMA.BusinessLogic.BLogic
{
    public class SubscriptionBL : SubscriptionApi, ISubscription
    {
        private readonly WebDbContext _db;
        private readonly IUserSession _session;

        public SubscriptionBL(WebDbContext db, IUserSession session)
        {
            _db = db;
            _session = session;
        }
        public bool HasActive()
        {
            var userId = _session.GetCurrentUserId();
            return HasActive(_db, userId);
        }
        public void Subscribe(PaymentViewModel dto)
        {
            var userId = _session.GetCurrentUserId();
            if (HasActive(_db, userId))
                return;

            var newSub = new Subscription
            {
                UserId = userId,
                StartDate = DateTime.UtcNow,
                EndDate = dto.SubscriptionType == "Monthly"
                        ? DateTime.UtcNow.AddMonths(1)
                        : DateTime.UtcNow.AddYears(1),
                Type = dto.SubscriptionType,
                IsActive = true
            };
            AddSubscription(_db, newSub);
        }
    }


    }
