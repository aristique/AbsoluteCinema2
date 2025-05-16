using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;
using ABSOLUTE_CINEMA.Models;
using System.Data.Entity;
using System.Web;

namespace ABSOLUTE_CINEMA.Controllers
{
    public class AccountController : Controller
    {
        private readonly WebDbContext _dbContext = new WebDbContext();

        // GET: Account/Register
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(Register model)
        {
            if (ModelState.IsValid)
            {
                if (_dbContext.Users.Any(u => u.Email == model.Email))
                {
                    ModelState.AddModelError("Email", "Email уже зарегистрирован");
                    return View(model);
                }

                using (var transaction = _dbContext.Database.BeginTransaction())
                {
                    try
                    {
                        var user = new User
                        {
                            Id = Guid.NewGuid(),
                            Name = model.Name,
                            Email = model.Email,
                            PasswordHash = BCrypt.Net.BCrypt.HashPassword(model.Password)
                        };

                        _dbContext.Users.Add(user);
                        _dbContext.SaveChanges();

                        var registeredRole = _dbContext.Roles.First(r => r.Name == "RegisteredUser");
                        _dbContext.UserRoles.Add(new UserRole
                        {
                            UserId = user.Id,
                            RoleId = registeredRole.Id
                        });

                        _dbContext.SaveChanges();
                        transaction.Commit();

                        var roles = GetUserRoles(user.Id);
                        SignInUser(user.Email, roles, false);

                        return RedirectToAction("Index", "Home");
                    }
                    catch
                    {
                        transaction.Rollback();
                        ModelState.AddModelError("", "Ошибка при регистрации");
                    }
                }
            }
            return View(model);
        }

        [HttpGet]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Login model)
        {
            if (ModelState.IsValid)
            {
                var user = _dbContext.Users.FirstOrDefault(u => u.Email == model.Email);

                if (user != null && BCrypt.Net.BCrypt.Verify(model.Password, user.PasswordHash))
                {
                    var roles = GetUserRoles(user.Id);
                    SignInUser(user.Email, roles, model.RememberMe);
                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError("", "Неверные учетные данные");
            }
            return View(model);
        }

        [Authorize]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

        [Authorize(Roles = "Admin")]
        public ActionResult UsersList()
        {
            var users = _dbContext.Users
                .Include("UserRoles.Role")
                .ToList()
                .Select(u => new UserViewModel
                {
                    Name = u.Name,
                    Email = u.Email,
                    Roles = string.Join(", ", u.UserRoles.Select(ur => ur.Role.Name))
                })
                .ToList();

            return View(users);
        }

        private List<string> GetUserRoles(Guid userId)
        {
            return _dbContext.UserRoles
                .Include(ur => ur.Role)
                .Where(ur => ur.UserId == userId)
                .Select(ur => ur.Role.Name)
                .ToList();
        }

        private void SignInUser(string email, List<string> roles, bool rememberMe)
        {
            var authTicket = new FormsAuthenticationTicket(
                1,
                email,
                DateTime.Now,
                rememberMe ? DateTime.Now.AddDays(7) : DateTime.Now.AddMinutes(30),
                rememberMe,
                string.Join("|", roles));

            string encryptedTicket = FormsAuthentication.Encrypt(authTicket);
            var authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
            Response.Cookies.Add(authCookie);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _dbContext.Dispose();
            }
            base.Dispose(disposing);
        }

        public class UserViewModel
        {
            public string Name { get; set; }
            public string Email { get; set; }
            public string Roles { get; set; }
        }
    }
}
