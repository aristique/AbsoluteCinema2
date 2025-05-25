using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using ABSOLUTE_CINEMA.BusinessLogic.BLogic;
using ABSOLUTE_CINEMA.Domain.Entities;
using ABSOLUTE_CINEMA.AbsoluteCinema.ViewModels;

namespace ABSOLUTE_CINEMA.Controllers
{
    [Authorize(Roles = "Admin")]
    public class MoviesController : Controller
    {
        private readonly MovieBL _movie = new MovieBL();

        public ActionResult Index()
        {
            var movies = _movie.GetAll();
            return View(movies);
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
            if (ModelState.IsValid)
            {
                var movie = new Movie
                {
                    Id = Guid.NewGuid(),
                    Title = model.Title,
                    Year = model.Year,
                    Country = model.Country,
                    Description = model.Description
                };

                _movie.Create(movie, model.SelectedGenres, model.SelectedActors, model.SelectedDirectors, youtubeId: null);
                return RedirectToAction("Index");
            }

            ViewBag.Genres = _movie.GetAvailableGenres();
            ViewBag.Actors = _movie.GetAvailableActors();
            ViewBag.Directors = _movie.GetAvailableDirectors();
            return View(model);
        }

        public ActionResult Edit(Guid id)
        {
            var movieEntity = _movie.Get(id);
            if (movieEntity == null) return HttpNotFound();

            var model = new MovieFormViewModel
            {
                Title = movieEntity.Title,
                Year = movieEntity.Year,
                Country = movieEntity.Country,
                Description = movieEntity.Description,
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
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Guid id, MovieFormViewModel model)
        {
            if (ModelState.IsValid)
            {
                var movie = _movie.Get(id);
                if (movie == null) return HttpNotFound();

                movie.Title = model.Title;
                movie.Year = model.Year;
                movie.Country = model.Country;
                movie.Description = model.Description;

                _movie.Update(movie, model.SelectedGenres, model.SelectedActors, model.SelectedDirectors);
                return RedirectToAction("Index");
            }

            ViewBag.Genres = _movie.GetAvailableGenres();
            ViewBag.Actors = _movie.GetAvailableActors();
            ViewBag.Directors = _movie.GetAvailableDirectors();
            return View(model);
        }

        public ActionResult Delete(Guid id)
        {
            var movieEntity = _movie.Get(id);
            if (movieEntity == null) return HttpNotFound();
            return View(movieEntity);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            _movie.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
