using System;
using System.Web.Mvc;
using ABSOLUTE_CINEMA.BusinessLogic.BLogic;

namespace ABSOLUTE_CINEMA.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        private readonly ProfileBL _profile = new ProfileBL();

        public ActionResult Index()
        {
            var email = User.Identity.Name;
            var model = _profile.GetProfile(email);
            return View(model);
        }
    }
}
