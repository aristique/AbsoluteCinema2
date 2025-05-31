using System;
using System.Linq;
using System.Web.Mvc;
using ABSOLUTE_CINEMA.BusinessLogic.Interfaces;
using ABSOLUTE_CINEMA.BusinessLogic.BLogic;
using ABSOLUTE_CINEMA.AbsoluteCinema.ViewModels;
using ABSOLUTE_CINEMA.Domain.Entities;

namespace ABSOLUTE_CINEMA.Controllers
{
    public class UserListController : Controller
    {
        private readonly IUserList _userList = new UserListBL();

        [HttpGet]
        public ActionResult Index()
        {
            var users = _userList.GetAllUsers(); 
            var roles = _userList.GetAllRoles();
            var subscriptionTypes = new[] { "Monthly", "Yearly" };

            var model = new UserListViewModel
            {
                Users = users.Select(user => new UserViewModel
                {
                    Id = user.Id,
                    Name = user.Name,
                    Email = user.Email,
                    Role = user.Roles // ← уже строка
                }).ToList(),

                AllRoles = roles.Select(role => new RoleViewModel
                {
                    Id = role.Id,
                    Name = role.Name
                }).ToList(),

                SubscriptionTypes = subscriptionTypes
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateRoles(Guid[] userIds, Guid[] roleIds)
        {
            _userList.UpdateRoles(userIds, roleIds);
            return RedirectToAction("Index");
        }
    }
}
