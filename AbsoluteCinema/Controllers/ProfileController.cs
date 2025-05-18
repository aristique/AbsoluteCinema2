// Controllers/ProfileController.cs
using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using ABSOLUTE_CINEMA.Domain.Entities;
using ABSOLUTE_CINEMA.Domain.DTO;

namespace ABSOLUTE_CINEMA.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        private readonly WebDbContext _db = new WebDbContext();

        public ActionResult Index()
        {
            var userEmail = User.Identity.Name;
            var user = _db.Users
                .Include(u => u.Subscriptions)
                .FirstOrDefault(u => u.Email == userEmail);

            if (user == null) return HttpNotFound();

            var activeSubscription = user.Subscriptions
                .FirstOrDefault(s => s.IsActive && s.EndDate >= DateTime.Now);

            var model = new ProfileViewModel
            {
                Name = user.Name,
                Email = user.Email,
                Subscription = activeSubscription
            };

            return View(model);
        }
    }
}