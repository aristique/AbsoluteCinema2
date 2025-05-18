using System;
using System.Collections.Generic;
using System.Linq;
using ABSOLUTE_CINEMA.Domain.Entities;

namespace ABSOLUTE_CINEMA.BusinessLogic.Core
{
    public class AdminApi
    {
        public void BanUser(WebDbContext db, Guid userId)
        {
            var oldRoles = db.UserRoles.Where(ur => ur.UserId == userId).ToList();
            if (oldRoles.Any()) db.UserRoles.RemoveRange(oldRoles);

            var banned = db.Roles.FirstOrDefault(r => r.Name == "Banned");
            if (banned != null)
            {
                db.UserRoles.Add(new UserRole { UserId = userId, RoleId = banned.Id });
                db.SaveChanges();
            }
        }
        public void ChangeUserRole(WebDbContext db, Guid userId, Guid roleId)
        {
            var old = db.UserRoles.Where(ur => ur.UserId == userId).ToList();
            if (old.Any()) db.UserRoles.RemoveRange(old);

            db.UserRoles.Add(new UserRole { UserId = userId, RoleId = roleId });
            db.SaveChanges();
        }
        public void DeleteComments(WebDbContext db, Guid userId)
        {
            var comms = db.Comments.Where(c => c.UserId == userId).ToList();
            if (comms.Any())
            {
                db.Comments.RemoveRange(comms);
                db.SaveChanges();
            }
        }
        public void GrantSubscription(WebDbContext db, Guid userId, string subscriptionType)
        {
            var active = db.Subscriptions
                           .FirstOrDefault(s => s.UserId == userId && s.IsActive && s.EndDate >= DateTime.UtcNow);
            if (active != null) return;

            var sub = new Subscription
            {
                UserId = userId,
                StartDate = DateTime.UtcNow,
                EndDate = subscriptionType == "Monthly"
                          ? DateTime.UtcNow.AddMonths(1)
                          : DateTime.UtcNow.AddYears(1),
                Type = subscriptionType,
                IsActive = true
            };
            db.Subscriptions.Add(sub);
            db.SaveChanges();
        }
    }
}
