using System;
using System.Web.Mvc;
using System.Web.Security;
using ABSOLUTE_CINEMA.BusinessLogic.BLogic;
using ABSOLUTE_CINEMA.Domain.DTO;
using ABSOLUTE_CINEMA.Domain.Entities;
using ABSOLUTE_CINEMA.AbsoluteCinema.ViewModels;

namespace ABSOLUTE_CINEMA.Controllers
{
    public class AccountController : Controller
    {
        private readonly AccountBL _account = new AccountBL();

        [HttpGet]
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public ActionResult Login()
        {
            FormsAuthentication.SignOut();
            return View();
        }
        [HttpPost]
        public ActionResult Login(Login model)
        {
            var userRole = _account.Login(model);
            if (userRole == UserRoleType.User)
            {
                ModelState.AddModelError("", "Неверные учетные данные");
                return View(model);
            }

            var session = new SessionBL();
            var userId = session.GetCurrentUserId();
            var email = model.Email;
            var role = session.GetCurrentUserRole();

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var dto = new Register
            {
                Name = model.Name,
                Email = model.Email,
                Password = model.Password,
                ConfirmPassword = model.ConfirmPassword
            };

            var result = _account.Register(dto);
            if (!result.Success)
            {
                ModelState.AddModelError("", result.Message);
                return View(model);
            }

            return RedirectToAction("Index", "Home");
        }

        [Authorize]
        public ActionResult Logout()
        {
            _account.SignOut();
            FormsAuthentication.SignOut();
            Session.Clear();
            Session.Abandon();
            return RedirectToAction("Index", "Home");
        }

        [Authorize(Roles = "Admin")]
        public ActionResult UsersList()
        {
            var users = _account.ListUsers();
            return View(users);
        }
    }
}
