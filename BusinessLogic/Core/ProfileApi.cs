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
                    .Include("Subscriptions")
                    .FirstOrDefault(u => u.Email == email);

                if (user == null) return null;

                var active = user.Subscriptions.FirstOrDefault(s => s.IsActive && s.EndDate >= System.DateTime.UtcNow);

                return new ProfileModel
                {
                    Name = user.Name,
                    Email = user.Email,
                    Subscription = active
                };
            }
        }
    }
}
