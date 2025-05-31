using System;
using System.Data.Entity;                    // Для Include(...)
using System.Linq;
using ABSOLUTE_CINEMA.Domain.DTO;
using ABSOLUTE_CINEMA.Domain.Entities;

namespace ABSOLUTE_CINEMA.BusinessLogic.Core
{
    public class ProfileApi
    {
        public ProfileModel Loadd(string email)
        {
            using (var db = new WebDbContext())
            {

                var user = db.Users
                    .Include(u => u.Subscriptions)
                    .FirstOrDefault(u => u.Email == email);

                if (user == null)
                    return null;

                var activeEntity = user.Subscriptions
                    .FirstOrDefault(s => s.IsActive && s.EndDate >= DateTime.UtcNow);

        
                SubscriptionModel activeDto = null;
                if (activeEntity != null)
                {
                    activeDto = new SubscriptionModel
                    {
                        Id = activeEntity.Id,
                        Type = activeEntity.Type,              
                        StartDate = activeEntity.StartDate,
                        EndDate = activeEntity.EndDate,
                        IsActive = activeEntity.IsActive

                    };
                }


                return new ProfileModel
                {
                    Name = user.Name,
                    Email = user.Email,
                    Subscription = activeDto
                };
            }
        }
    }
}
