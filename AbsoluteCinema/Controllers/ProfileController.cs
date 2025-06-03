using System.Linq;
using System.Web.Mvc;
using ABSOLUTE_CINEMA.BusinessLogic.BLogic;
using ABSOLUTE_CINEMA.BusinessLogic.Core;
using ABSOLUTE_CINEMA.AbsoluteCinema.ViewModels;
using ABSOLUTE_CINEMA.Domain.DTO;
using ABSOLUTE_CINEMA.BusinessLogic.Interfaces;
using System;

namespace ABSOLUTE_CINEMA.Controllers
{
    public class ProfileController : Controller
    {
        private readonly IProfile _profileApi = new ProfileBL();
        private readonly IHistory _historyService = new HistoryBL();
        private readonly IUserSession _session = new SessionBL();

        [HttpGet]
        [Authorize]
        public ActionResult Index()
        {

            var email = User.Identity.Name;
            var dto = _profileApi.GetProfile(email);
            if (dto == null)
                return HttpNotFound();


            var viewModel = new ProfileViewModel
            {
                Name = dto.Name,
                Email = dto.Email,
                Subscription = dto.Subscription
            };


            var userId = _session.GetCurrentUserId();
            if (userId != Guid.Empty)
            {

                var historyDtos = _historyService.GetByUser(userId, limit: 50);

                viewModel.History = historyDtos.Select(h => new HistoryItemViewModel
                {
                    MovieId = h.MovieId,
                    MovieTitle = h.MovieTitle,
                    YouTubeVideoId = h.YouTubeVideoId,
                    ViewedAt = h.ViewedAt
                })
                .ToList();
            }
            else
            {
                viewModel.History = Enumerable.Empty<HistoryItemViewModel>();
            }

            return View(viewModel);

        }
    
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult ClearHistory()
        {

            Guid userId = _session.GetCurrentUserId();

            if (userId != Guid.Empty)
            {

                _historyService.DeleteByUser(userId);
            }

            return RedirectToAction("Index");
        }
    }
}
