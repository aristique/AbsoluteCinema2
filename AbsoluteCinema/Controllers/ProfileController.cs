using System;
using System.Web.Mvc;
using ABSOLUTE_CINEMA.BusinessLogic.Interfaces;
using ABSOLUTE_CINEMA.Domain.DTO;

namespace ABSOLUTE_CINEMA.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        private readonly IProfile _profile;

        public ProfileController(IProfile profile)
        {
            _profile = profile;
        }

        public ActionResult Index()
        {
            var email = User.Identity.Name;
            var model = _profile.GetProfile(email);
            return View(model);
        }
    }
}