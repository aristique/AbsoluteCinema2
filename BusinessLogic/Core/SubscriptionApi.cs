using System;
using System.Linq;
using ABSOLUTE_CINEMA;
using ABSOLUTE_CINEMA.Domain.Entities;

namespace BusinessLogic.Core
{
    public class SubscriptionApi
    {
        public bool HasActive(WebDbContext db,Guid userId)
        {
            return db.Subscriptions.Any(s=>s.UserId==userId && s.IsActive && s.EndDate >= DateTime.Now);
        }
        public void AddSubscription(WebDbContext db, Subscription entity)
        {
            db.Subscriptions.Add(entity);
            db.SaveChanges();
        }
        
    }
}
