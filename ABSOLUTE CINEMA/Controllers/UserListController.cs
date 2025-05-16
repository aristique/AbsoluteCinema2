using System.Linq;
using System.Web.Mvc;
using ABSOLUTE_CINEMA.Models;
using System.Data.Entity;
using System;

namespace ABSOLUTE_CINEMA.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UserListController : Controller
    {
        private readonly WebDbContext _db = new WebDbContext();

        public ActionResult Index()
        {
            var users = _db.Users
                .Include(u => u.UserRoles.Select(ur => ur.Role))
                .ToList();

            var allRoles = _db.Roles.ToList();
            ViewBag.AllRoles = allRoles;

            return View(users);
        }

        [HttpPost]
        public ActionResult UpdateRoles(Guid[] userIds, Guid[] roleIds)
        {
            if (userIds.Length != roleIds.Length)
                return new HttpStatusCodeResult(400, "Данные не совпадают");

            for (int i = 0; i < userIds.Length; i++)
            {
                var userId = userIds[i];
                var roleId = roleIds[i];

                // Удаляем старые роли пользователя
                var oldRoles = _db.UserRoles.Where(ur => ur.UserId == userId).ToList();
                _db.UserRoles.RemoveRange(oldRoles);

                // Назначаем новую роль
                _db.UserRoles.Add(new UserRole
                {
                    UserId = userId,
                    RoleId = roleId
                });
            }

            _db.SaveChanges();

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
