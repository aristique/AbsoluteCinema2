using System.Web.Mvc;
using ABSOLUTE_CINEMA.AbsoluteCinema.ViewModels;
using ABSOLUTE_CINEMA.BusinessLogic.BLogic;
using ABSOLUTE_CINEMA.BusinessLogic.Interfaces;
using ABSOLUTE_CINEMA.Domain.DTO;

namespace ABSOLUTE_CINEMA.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        private readonly IProfile _profile = new ProfileBL();

        public ActionResult Index()
        {
            var profileData = _profile.GetProfile(User.Identity.Name);

            var profileViewModel = new ProfileViewModel
            {
                Name = profileData.Name,
                Email = profileData.Email,
                Subscription = profileData.Subscription
            };

            return View(profileViewModel);
        }
    }
}
