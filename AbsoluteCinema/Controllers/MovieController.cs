using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using ABSOLUTE_CINEMA.BusinessLogic.BLogic;
using ABSOLUTE_CINEMA.Domain.Entities;
using ABSOLUTE_CINEMA.AbsoluteCinema.ViewModels;
using ABSOLUTE_CINEMA.BusinessLogic.Attributes;

namespace ABSOLUTE_CINEMA.Controllers
{
    [CustomAuthorize(Roles = "Admin")]
    public class MoviesController : Controller
    {
        private readonly MovieBL _movie = new MovieBL();

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
        public ActionResult Create(MovieFormViewModel model)
        {
            if (ModelState.IsValid)
            {
                var movie = new Movie
                {
                    Id = Guid.NewGuid(),
                    Title = model.Title,
                    Year = model.Year,
                    Country = model.Country,
                    Description = model.Description,
                    YouTubeVideoId = model.YouTubeVideoId
                };

                _movie.Create(movie,
                              model.SelectedGenres,
                              model.SelectedActors,
                              model.SelectedDirectors,
                              model.YouTubeVideoId);
                return RedirectToAction("Index", "Catalog");
            }

            ViewBag.Genres = _movie.GetAvailableGenres();
            ViewBag.Actors = _movie.GetAvailableActors();
            ViewBag.Directors = _movie.GetAvailableDirectors();
            return View(model);
        }

        public ActionResult Edit(Guid id)
        {
            var movieEntity = _movie.Get(id);
            if (movieEntity == null)
                return HttpNotFound();

            var model = new MovieFormViewModel
            {
                Id = movieEntity.Id,
                Title = movieEntity.Title,
                Year = movieEntity.Year,
                Country = movieEntity.Country,
                Description = movieEntity.Description,
                YouTubeVideoId = movieEntity.YouTubeVideoId,
                SelectedGenres = movieEntity.Genres.Select(g => g.Genre.Id).ToList(),
                SelectedActors = movieEntity.Actors.Select(a => a.Actor.Id).ToList(),
                SelectedDirectors = movieEntity.Directors.Select(d => d.Director.Id).ToList()
            };

            ViewBag.Genres = _movie.GetAvailableGenres();
            ViewBag.Actors = _movie.GetAvailableActors();
            ViewBag.Directors = _movie.GetAvailableDirectors();
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(Guid id, MovieFormViewModel model)
        {
            if (ModelState.IsValid)
            {
                var movie = _movie.Get(id);
                if (movie == null)
                    return HttpNotFound();

                movie.Title = model.Title;
                movie.Year = model.Year;
                movie.Country = model.Country;
                movie.Description = model.Description;
                movie.YouTubeVideoId = model.YouTubeVideoId;

                _movie.Update(movie,
                              model.SelectedGenres,
                              model.SelectedActors,
                              model.SelectedDirectors);
                return RedirectToAction("Index", "Catalog");
            }

            ViewBag.Genres = _movie.GetAvailableGenres();
            ViewBag.Actors = _movie.GetAvailableActors();
            ViewBag.Directors = _movie.GetAvailableDirectors();
            return View(model);
        }

        public ActionResult Delete(Guid id)
        {
            var entity = _movie.Get(id);
            if (entity == null)
                return HttpNotFound();

            var vm = new MovieViewModel
            {
                Id = entity.Id,
                Title = entity.Title,
                Year = entity.Year,
                YouTubeVideoId = entity.YouTubeVideoId
            };

            return View(vm);
        }

        
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            _movie.Delete(id);
            return RedirectToAction("Index", "Catalog");
        }
    }
}
