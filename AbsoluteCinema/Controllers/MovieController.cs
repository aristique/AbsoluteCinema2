using System;
using System.Collections.Generic;
using System.Web.Mvc;
using ABSOLUTE_CINEMA.BusinessLogic.Interfaces;
using ABSOLUTE_CINEMA.Domain.Entities;
using System.Linq;

namespace ABSOLUTE_CINEMA.Controllers
{
    [Authorize(Roles = "Admin")]
    public class MoviesController : Controller
    {
        private readonly IMovie _movie;

        public MoviesController(IMovie movie)
        {
            _movie = movie;
        }

        public ActionResult Index()
        {
            var movies = _movie.GetAll();
            return View(movies);
        }

        public ActionResult Create()
        {
            PopulateSelects();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Movie movie, List<Guid> SelectedGenres, List<Guid> SelectedActors, List<Guid> SelectedDirectors, string youtubeInput)
        {
            if (ModelState.IsValid)
            {
                _movie.Create(movie, SelectedGenres, SelectedActors, SelectedDirectors, youtubeInput);
                return RedirectToAction("Index");
            }
            PopulateSelects();
            return View(movie);
        }

        public ActionResult Edit(Guid id)
        {
            var movieEntity = _movie.Get(id);
            if (movieEntity == null) return HttpNotFound();

            PopulateSelects();
            return View(movieEntity);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Movie movie, List<Guid> SelectedGenres, List<Guid> SelectedActors, List<Guid> SelectedDirectors)
        {
            if (ModelState.IsValid)
            {
                _movie.Update(movie, SelectedGenres, SelectedActors, SelectedDirectors);
                return RedirectToAction("Index");
            }
            PopulateSelects();
            return View(movie);
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

        private void PopulateSelects()
        {
            ViewBag.Genres = new SelectList(_movie.GetAll()
                .SelectMany(m => m.Genres)
                .Select(g => g.Genre)
                .Distinct(), "Id", "Name");

            ViewBag.Actors = new SelectList(_movie.GetAll()
                .SelectMany(m => m.Actors)
                .Select(a => a.Actor)
                .Distinct(), "Id", "Name");

            ViewBag.Directors = new SelectList(_movie.GetAll()
                .SelectMany(m => m.Directors)
                .Select(d => d.Director)
                .Distinct(), "Id", "Name");
        }
    }
}