using System.Web.Mvc;
using ABSOLUTE_CINEMA.AbsoluteCinema.ViewModels;
using ABSOLUTE_CINEMA.BusinessLogic.BLogic;
using ABSOLUTE_CINEMA.Domain.DTO;

namespace ABSOLUTE_CINEMA.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        private readonly ProfileBL _profile = new ProfileBL();

        public ActionResult Index()
        {
            
            ProfileModel dto = _profile.GetProfile(User.Identity.Name);

            
            var vm = new ProfileViewModel
            {
                Name = dto.Name,
                Email = dto.Email,
                Subscription = dto.Subscription
            };

            
            return View(vm);
        }
    }
}
