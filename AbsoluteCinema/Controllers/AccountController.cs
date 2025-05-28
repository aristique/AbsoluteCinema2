using ABSOLUTE_CINEMA.AbsoluteCinema.ViewModels;
using System.Linq;
using ABSOLUTE_CINEMA.BusinessLogic.Attributes;
using ABSOLUTE_CINEMA.BusinessLogic.BLogic;
using ABSOLUTE_CINEMA.BusinessLogic.Interfaces;
using System.Web.Mvc;

namespace ABSOLUTE_CINEMA.AbsoluteCinema.Controllers
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
        public ActionResult Login(LoginViewModel loginModel)
        {
            if (!ModelState.IsValid)
            {
                return View(loginModel);
            }

            var userRole = _accountService.Login(new Domain.DTO.Login
            {
                Email = loginModel.Email,
                Password = loginModel.Password
            });

            if (userRole == Domain.Entities.UserRoleType.None)
            {
                ModelState.AddModelError(string.Empty, "Неверные учётные данные");
                return View(loginModel);
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
        public ActionResult Register(RegisterViewModel registerModel)
        {
            if (!ModelState.IsValid)
            {
                return View(registerModel);
            }

            if (registerModel.Password != registerModel.ConfirmPassword)
            {
                ModelState.AddModelError(string.Empty, "Пароли не совпадают");
                return View(registerModel);
            }

            var registrationResult = _accountService.Register(new Domain.DTO.Registerr
            {
                Name = registerModel.Name,
                Email = registerModel.Email,
                Password = registerModel.Password,
                ConfirmPassword = registerModel.ConfirmPassword
            });

            if (!registrationResult.Success)
            {
                ModelState.AddModelError(string.Empty, registrationResult.Message);
                return View(registerModel);
            }

            _accountService.SignIn(
                registrationResult.UserId,
                registerModel.Email,
                Domain.Entities.UserRoleType.RegisteredUser
            );

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
            var allUsers = _accountService.ListUsers();
            var userViewModels = allUsers
                .Select(user => new UserViewModel
                {
                    Id = user.Id,
                    Name = user.Name,
                    Email = user.Email,
                    Role = user.Roles
                })
                .ToList();

            return View(userViewModels);
        }
    }
}
