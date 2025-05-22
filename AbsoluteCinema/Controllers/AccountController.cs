// Controllers/AccountController.cs
using System;
using System.Web.Mvc;
using System.Web.Security;
using ABSOLUTE_CINEMA.BusinessLogic.Interfaces;
using ABSOLUTE_CINEMA.Domain.DTO;
using ABSOLUTE_CINEMA.Domain.Entities;

namespace ABSOLUTE_CINEMA.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccount _account;

        public AccountController(IAccount account)
        {
            _account = account;
        }

        [HttpGet]
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public ActionResult Login()
        {
            // Сбрасываем старые куки при заходе на страницу логина
            FormsAuthentication.SignOut();
            return View();
        }

        [HttpPost]
        public ActionResult Login(Login model)
        {
            // Проверка учетных данных без валидации модели
            var userRole = _account.Login(model);
            if (userRole == UserRoleType.User)
            {
                ModelState.AddModelError("", "Неверные учетные данные");
                return View(model);
            }

            // Получаем идентификатор пользователя и создаем аутентификационную куку
            var userId = _account.GetUserIdByEmail(model.Email);
            _account.SignIn(userId, model.Email, userRole);

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(Register model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var result = _account.Register(model);
            if (!result.Success)
            {
                ModelState.AddModelError("", result.Message);
                return View(model);
            }

            // Логиним сразу после регистрации
            _account.SignIn(result.UserId, model.Email, UserRoleType.User);

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
