using System;
using System.Web.Mvc;
using ABSOLUTE_CINEMA.BusinessLogic.BLogic;
using ABSOLUTE_CINEMA.BusinessLogic.Attributes;

namespace ABSOLUTE_CINEMA.Controllers
{
    [CustomAuthorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly AdminBL _admin = new AdminBL();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Ban(Guid userId)
        {
            _admin.BanUser(userId);
            return RedirectToAction("Index", "UserList");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangeRole(Guid userId, Guid roleId)
        {
            _admin.ChangeUserRole(userId, roleId);
            return RedirectToAction("Index", "UserList");
        }

        // POST: /Admin/DeleteComments
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteComments(Guid userId)
        {
            _admin.DeleteComments(userId);
            return RedirectToAction("Index", "UserList");
        }

 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult GrantSubscription(Guid userId, string subscriptionType)
        {
            _admin.GrantSubscription(userId, subscriptionType);
            return RedirectToAction("Index", "UserList");
        }
    }
}
