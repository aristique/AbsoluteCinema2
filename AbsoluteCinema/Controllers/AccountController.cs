using System.Linq;
using System.Web.Mvc;
using ABSOLUTE_CINEMA.AbsoluteCinema.ViewModels;
using ABSOLUTE_CINEMA.BusinessLogic.BLogic;
using ABSOLUTE_CINEMA.BusinessLogic.Interfaces;
using ABSOLUTE_CINEMA.Domain.DTO;
using ABSOLUTE_CINEMA.BusinessLogic.Attributes;

namespace ABSOLUTE_CINEMA.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccount _accountService = new AccountBL();

        [HttpGet]
        public ActionResult Login()
        {
            return View(new LoginViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var loginDto = new Login
            {
                Email = model.Email,
                Password = model.Password
            };

            var role = _accountService.Login(loginDto);

            if (role == Domain.Entities.UserRoleType.None)
            {
                ModelState.AddModelError(string.Empty, "Неверные учётные данные");
                return View(model);
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View(new RegisterViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            if (model.Password != model.ConfirmPassword)
            {
                ModelState.AddModelError("", "Пароли не совпадают");
                return View(model);
            }

            var registerDto = new Registerr
            {
                Name = model.Name,
                Email = model.Email,
                Password = model.Password,
                ConfirmPassword = model.ConfirmPassword
            };

            var result = _accountService.Register(registerDto);
            if (!result.Success)
            {
                ModelState.AddModelError("", result.Message);
                return View(model);
            }

            _accountService.SignIn(result.UserId, model.Email, Domain.Entities.UserRoleType.RegisteredUser);
            return RedirectToAction("Index", "Home");
        }

        [CustomAuthorize]
        public ActionResult Logout()
        {
            _accountService.SignOut();
            return RedirectToAction("Index", "Home");
        }

        [CustomAuthorize(Roles = "Admin")]
        public ActionResult UsersList()
        {
            var users = _accountService.ListUsers();

            var viewModels = users.Select(u => new UserViewModel
            {
                Id = u.Id,
                Name = u.Name,
                Email = u.Email,
                Role = u.Role
            }).ToList();

            return View(viewModels);
        }
    }
}
