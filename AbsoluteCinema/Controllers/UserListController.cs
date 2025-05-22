using System;
using System.Collections.Generic;
using System.Web.Mvc;
using ABSOLUTE_CINEMA.BusinessLogic.Interfaces;
using ABSOLUTE_CINEMA.Domain.Entities;

namespace ABSOLUTE_CINEMA.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UserListController : Controller
    {
        private readonly IUserList _userList;

        public UserListController(IUserList userList)
        {
            _userList = userList;
        }

        public ActionResult Index()
        {
            var users = _userList.GetAllUsers();
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