using System;
using System.Linq;
using ABSOLUTE_CINEMA.Domain.Entities;


namespace ABSOLUTE_CINEMA.BusinessLogic.Core
{
    public class SubscriptionApi
    {
        public bool HasActive(Guid userId)
        {
            using (var db = new WebDbContext())
            {
                return db.Subscriptions.Any(s => s.UserId == userId && s.IsActive && s.EndDate >= DateTime.UtcNow);
            }
        }

        public void AddSubscription(Subscription entity)
        {
            using (var db = new WebDbContext())
            {
                db.Subscriptions.Add(entity);
                db.SaveChanges();
            }
        }
    }
}
