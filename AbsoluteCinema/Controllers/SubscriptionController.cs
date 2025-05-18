using System;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using ABSOLUTE_CINEMA.Domain.Entities;
using ABSOLUTE_CINEMA.Domain.DTO;

namespace ABSOLUTE_CINEMA.Controllers
{
    [Authorize]
    public class SubscriptionController : Controller
    {
        private readonly WebDbContext _db = new WebDbContext();

        // GET: /Subscription/Subscripe
        public ActionResult Subscripe()
        {
            var hasSubscription = HasActiveSubscription();
            ViewBag.HasActiveSubscription = hasSubscription;

            if (hasSubscription)
            {
                TempData["SubscriptionMessage"] = "У вас уже есть активная подписка.";
                return RedirectToAction("Index", "Profile");
            }

            return View();
        }

        // POST: /Subscription/Subscripe
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Subscripe(PaymentViewModel model)
        {
            var hasSubscription = HasActiveSubscription();
            ViewBag.HasActiveSubscription = hasSubscription;

            if (hasSubscription)
            {
                TempData["SubscriptionMessage"] = "У вас уже есть активная подписка.";
                return RedirectToAction("Index", "Profile");
            }

            if (!ModelState.IsValid)
                return View(model);

            var user = _db.Users
                .Include(u => u.Subscriptions)
                .FirstOrDefault(u => u.Email == User.Identity.Name);

            if (user == null)
                return HttpNotFound();

            // Деактивируем все предыдущие подписки
            foreach (var sub in user.Subscriptions.Where(s => s.IsActive))
            {
                sub.IsActive = false;
            }

            // Создаем новую подписку
            var newSub = new Subscription
            {
                UserId = user.Id,
                StartDate = DateTime.Now,
                EndDate = model.SubscriptionType == "Monthly"
                    ? DateTime.Now.AddMonths(1)
                    : DateTime.Now.AddYears(1),
                Type = model.SubscriptionType,
                IsActive = true
            };

            _db.Subscriptions.Add(newSub);

            // Назначаем роль, если нужно
            var role = _db.Roles.FirstOrDefault(r => r.Name == "SubscribedUser");
            if (role != null && !_db.UserRoles.Any(ur => ur.UserId == user.Id && ur.RoleId == role.Id))
            {
                _db.UserRoles.Add(new UserRole { UserId = user.Id, RoleId = role.Id });
            }

            _db.SaveChanges();

            // Обновляем куку авторизации с новыми ролями
            var roles = _db.UserRoles
                .Where(ur => ur.UserId == user.Id)
                .Select(ur => ur.Role.Name)
                .ToList();

            FormsAuthentication.SignOut();

            var ticket = new FormsAuthenticationTicket(
                1,
                user.Email,
                DateTime.Now,
                DateTime.Now.AddMinutes(30),
                false,
                string.Join("|", roles)
            );

            var encTicket = FormsAuthentication.Encrypt(ticket);
            Response.Cookies.Add(new HttpCookie(FormsAuthentication.FormsCookieName, encTicket));

            return RedirectToAction("Index", "Profile");
        }

        // Проверка: есть ли активная подписка
        private bool HasActiveSubscription()
        {
            var email = User.Identity.Name;

            var user = _db.Users
                .Include(u => u.Subscriptions)
                .FirstOrDefault(u => u.Email == email);

            return user?.Subscriptions?
                .Any(s => s.IsActive && s.EndDate >= DateTime.Now) ?? false;
        }
    }
}
