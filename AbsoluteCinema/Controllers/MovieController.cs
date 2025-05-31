using System;
using System.Linq;
using System.Web.Mvc;
using ABSOLUTE_CINEMA.AbsoluteCinema.ViewModels;
using ABSOLUTE_CINEMA.BusinessLogic.Attributes;
using ABSOLUTE_CINEMA.BusinessLogic.BLogic;
using ABSOLUTE_CINEMA.BusinessLogic.Interfaces;
using ABSOLUTE_CINEMA.Domain.DTO;

namespace ABSOLUTE_CINEMA.Controllers
{
    [CustomAuthorize(Roles = "Admin")]
    public class MoviesController : Controller
    {
        private readonly IMovie _movie = new MovieBL();

        public ActionResult Index()
        {
            var movies = _movie.GetAll();
            var viewModels = movies.Select(m => new MovieFormViewModel
            {
                Id = m.Id,
                Title = m.Title,
                Year = m.Year,
                Country = m.Country,
                Description = m.Description,
                YouTubeVideoId = m.YouTubeVideoId
            }).ToList();
            return View(viewModels);
        }

        public ActionResult Create()
        {
            ViewBag.Genres = _movie.GetAvailableGenres();
            ViewBag.Actors = _movie.GetAvailableActors();
            ViewBag.Directors = _movie.GetAvailableDirectors();
            return View(new MovieFormViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(MovieFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Genres = _movie.GetAvailableGenres();
                ViewBag.Actors = _movie.GetAvailableActors();
                ViewBag.Directors = _movie.GetAvailableDirectors();
                return View(model);
            }

            var dto = new CreateMovieModel
            {
                Title = model.Title,
                Year = model.Year,
                Country = model.Country,
                Description = model.Description,
                YouTubeVideoId = model.YouTubeVideoId,
                SelectedGenres = model.SelectedGenres,
                SelectedActors = model.SelectedActors,
                SelectedDirectors = model.SelectedDirectors
            };

            _movie.Create(dto);
            return RedirectToAction("Index", "Catalog");
        }

        public ActionResult Edit(Guid id)
        {
            var movie = _movie.Get(id);
            if (movie == null)
                return HttpNotFound();

            var model = new MovieFormViewModel
            {
                Id = movie.Id,
                Title = movie.Title,
                Year = movie.Year,
                Country = movie.Country,
                Description = movie.Description,
                YouTubeVideoId = movie.YouTubeVideoId,
                SelectedGenres = movie.Genres.Select(g => g.Id).ToList(),
                SelectedActors = movie.Actors.Select(a => a.Id).ToList(),
                SelectedDirectors = movie.Directors.Select(d => d.Id).ToList()
            };

            ViewBag.Genres = _movie.GetAvailableGenres();
            ViewBag.Actors = _movie.GetAvailableActors();
            ViewBag.Directors = _movie.GetAvailableDirectors();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Guid id, MovieFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Genres = _movie.GetAvailableGenres();
                ViewBag.Actors = _movie.GetAvailableActors();
                ViewBag.Directors = _movie.GetAvailableDirectors();
                return View(model);
            }

            var dto = new CreateMovieModel
            {
                Title = model.Title,
                Year = model.Year,
                Country = model.Country,
                Description = model.Description,
                YouTubeVideoId = model.YouTubeVideoId,
                SelectedGenres = model.SelectedGenres,
                SelectedActors = model.SelectedActors,
                SelectedDirectors = model.SelectedDirectors
            };

            _movie.Update(id, dto);
            return RedirectToAction("Index", "Catalog");
        }

        [HttpGet]
        public ActionResult Delete(Guid id)
        {
            var movie = _movie.Get(id);
            if (movie == null)
                return HttpNotFound();

            var viewModel = new MovieViewModel
            {
                Id = movie.Id,
                Title = movie.Title,
                Year = movie.Year,

                YouTubeVideoId = movie.YouTubeVideoId
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Guid id, FormCollection form)
        {
            _movie.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
