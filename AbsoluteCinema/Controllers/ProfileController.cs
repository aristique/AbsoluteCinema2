using System.Web.Mvc;
using ABSOLUTE_CINEMA.BusinessLogic.Core;
using ABSOLUTE_CINEMA.AbsoluteCinema.ViewModels;
using ABSOLUTE_CINEMA.Domain.DTO;

namespace ABSOLUTE_CINEMA.Controllers
{
    public class ProfileController : Controller
    {
        private readonly ProfileApi _profileApi = new ProfileApi();

        [HttpGet]
        [Authorize]
        public ActionResult Index()
        {
            var email = User.Identity.Name;
            var dto = _profileApi.Loadd(email);
            if (dto == null)
                return HttpNotFound();
            var viewModel = new ProfileViewModel
            {
                Name = dto.Name,
                Email = dto.Email,
                Subscription = dto.Subscription
            };
            return View(viewModel);
        }
    }
}
