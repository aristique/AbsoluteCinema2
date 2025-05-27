using ABSOLUTE_CINEMA.AbsoluteCinema.ViewModels;
using System.Linq;
using ABSOLUTE_CINEMA.BusinessLogic.Attributes;
using ABSOLUTE_CINEMA.BusinessLogic.BLogic;
using ABSOLUTE_CINEMA.Domain.Entities;
using ABSOLUTE_CINEMA.Domain.DTO;
using System.Web.Mvc;

public class AccountController : Controller
{
    private readonly AccountBL _account = new AccountBL();

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

        var role = _account.Login(new Login
        {
            Email = model.Email,
            Password = model.Password
        });

        if (role == UserRoleType.None)
        {
            ModelState.AddModelError("", "Неверные учётные данные");
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

        var result = _account.Register(new Registerr
        {
            Name = model.Name,
            Email = model.Email,
            Password = model.Password,
            ConfirmPassword = model.ConfirmPassword
        });

        if (!result.Success)
        {
            ModelState.AddModelError("", result.Message);
            return View(model);
        }

        _account.SignIn(result.UserId, model.Email, UserRoleType.RegisteredUser);
        return RedirectToAction("Index", "Home");
    }

    [CustomAuthorize]
    public ActionResult Logout()
    {
        _account.SignOut();
        return RedirectToAction("Index", "Home");
    }

    [CustomAuthorize(Roles = "Admin")]
    public ActionResult UsersList()
    {
        var usersDto = _account.ListUsers();
        var vm = usersDto.Select(u => new UserViewModel
        {
            Id = u.Id,
            Name = u.Name,
            Email = u.Email,
            Role = u.Roles
        }).ToList();

        return View(vm);
    }
}
