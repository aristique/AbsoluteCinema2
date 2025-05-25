using System;
using System.Collections.Generic;
using System.Web.Mvc;
using ABSOLUTE_CINEMA.BusinessLogic.BLogic;
using ABSOLUTE_CINEMA.Domain.Entities;

namespace ABSOLUTE_CINEMA.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UserListController : Controller
    {
        private readonly UserListBL _userList = new UserListBL();

        public ActionResult Index()
        {
            var users = _userList.GetAllUsers();
            ViewBag.AllRoles = _userList.GetAllRoles();
            return View(users);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateRoles(Guid[] userIds, Guid[] roleIds)
        {
            if (userIds == null || roleIds == null || userIds.Length != roleIds.Length)
                return new HttpStatusCodeResult(400);

            _userList.UpdateRoles(userIds, roleIds);
            return RedirectToAction("Index");
        }
    }
}
