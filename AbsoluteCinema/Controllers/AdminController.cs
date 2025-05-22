using System;
using System.Web.Mvc;
using ABSOLUTE_CINEMA.BusinessLogic.Interfaces;
using ABSOLUTE_CINEMA.Domain.Entities;

namespace ABSOLUTE_CINEMA.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly IAdmin _admin;

        public AdminController(IAdmin admin)
        {
            _admin = admin;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Ban(Guid userId)
        {
            _admin.BanUser(userId);
            return RedirectToAction("UsersList", "Account");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangeRole(Guid userId, Guid roleId)
        {
            _admin.ChangeUserRole(userId, roleId);
            return RedirectToAction("UsersList", "Account");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteComments(Guid userId)
        {
            _admin.DeleteComments(userId);
            return RedirectToAction("UsersList", "Account");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult GrantSubscription(Guid userId, string subscriptionType)
        {
            _admin.GrantSubscription(userId, subscriptionType);
            return RedirectToAction("UsersList", "Account");
        }
    }
}