using System;
using System.Collections.Generic;
using System.Linq;
using ABSOLUTE_CINEMA.Domain.Entities;

namespace ABSOLUTE_CINEMA.BusinessLogic.Core
{
    public class AdminApi
    {
        public void BanUserr(Guid userId)
        {
            using (var db = new WebDbContext())
            {
                var oldRoles = db.UserRoles.Where(ur => ur.UserId == userId).ToList();
                if (oldRoles.Any()) db.UserRoles.RemoveRange(oldRoles);

                var banned = db.Roles.FirstOrDefault(r => r.Name == "Guest");
                if (banned != null)
                {
                    db.UserRoles.Add(new UserRole { UserId = userId, RoleId = banned.Id });
                    db.SaveChanges();
                }
            }
        }

        public void ChangeUserRolee(Guid userId, Guid roleId)
        {
            using (var db = new WebDbContext())
            {
                var old = db.UserRoles.Where(ur => ur.UserId == userId).ToList();
                if (old.Any()) db.UserRoles.RemoveRange(old);

                db.UserRoles.Add(new UserRole { UserId = userId, RoleId = roleId });
                db.SaveChanges();
            }
        }

        public void DeleteComment(Guid commentId)
        {
            using (var db = new WebDbContext())
            {
                var comm = db.Comments.Find(commentId);
                if (comm != null)
                {
                    db.Comments.Remove(comm);
                    db.SaveChanges();
                }
            }
        }


        public void GrantSubscriptionn(Guid userId, string subscriptionType)
        {
            using (var db = new WebDbContext())
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
}
