using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web.Mvc;
using ABSOLUTE_CINEMA.Models;

namespace ABSOLUTE_CINEMA.Controllers
{
    
        [Authorize(Roles = "Admin")]
        public class MoviesController : Controller
        {
            private readonly WebDbContext _db = new WebDbContext();

            // GET: Movies
            public ActionResult Index()
            {
                var movies = _db.Movies
                    .Include(m => m.Genres.Select(g => g.Genre))
                    .ToList();
                return View(movies);
            }

            // GET: Movies/Create
            public ActionResult Create()
            {
                ViewBag.Genres = _db.Genres.ToList();
                ViewBag.Actors = _db.Actors.ToList();
                ViewBag.Directors = _db.Directors.ToList();
                return View();
            }

            // В контроллере MoviesController
            [HttpPost]
            [ValidateAntiForgeryToken]
            public ActionResult Create(Movie movie, 
                List<Guid> SelectedGenres,
                List<Guid> SelectedActors,
                List<Guid> SelectedDirectors, 
                string youtubeInput)
            {
                ModelState.Remove("YouTubeVideoId");
                movie.YouTubeVideoId = youtubeInput;

                if (ModelState.IsValid)
                {
                    try
                    {
                        // Инициализация коллекций
                        movie.Genres = new List<MovieGenre>();
                        movie.Actors = new List<MovieActor>();
                        movie.Directors = new List<MovieDirector>();

                        AddRelations(movie, SelectedGenres, SelectedActors, SelectedDirectors);

                        _db.Movies.Add(movie);
                        _db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                    catch (Exception ex)
                    {
                        ModelState.AddModelError("", "Ошибка: " + ex.Message);
                    }
                }

                ViewBag.Genres = _db.Genres.ToList();
                ViewBag.Actors = _db.Actors.ToList();
                ViewBag.Directors = _db.Directors.ToList();
                return View(movie);
            }
            

            private void AddRelations(Movie movie, List<Guid> genres, List<Guid> actors, List<Guid> directors)
            {
                movie.Genres = genres?.Select(g => new MovieGenre { GenreId = g }).ToList() 
                               ?? new List<MovieGenre>();

                movie.Actors = actors?.Select(a => new MovieActor { ActorId = a }).ToList() 
                               ?? new List<MovieActor>();

                movie.Directors = directors?.Select(d => new MovieDirector { DirectorId = d }).ToList() 
                                  ?? new List<MovieDirector>();
            }
            
            
            public ActionResult Details(Guid id)
            {
                try
                {
                    var movie = _db.Movies
                        .Include(m => m.Genres.Select(g => g.Genre))
                        .FirstOrDefault(m => m.Id == id);

                    if (movie == null) return HttpNotFound();
        
                    if (string.IsNullOrWhiteSpace(movie.YouTubeVideoId))
                    {
                        ViewBag.VideoError = "Видео недоступно";
                    }

                    return View(movie);
                }
                catch (Exception ex)
                {
                    // Логирование ошибки
                    return View("Error");
                }
            }

            public ActionResult Edit(Guid id)
            {
                var movie = _db.Movies
                    .Include(m => m.Genres)
                    .Include(m => m.Actors)
                    .Include(m => m.Directors)
                    .FirstOrDefault(m => m.Id == id);

                if (movie == null) return HttpNotFound();

                ViewBag.Genres = _db.Genres.ToList();
                ViewBag.Actors = _db.Actors.ToList();
                ViewBag.Directors = _db.Directors.ToList();

                return View(movie);
            }

            [HttpPost]
            [ValidateAntiForgeryToken]
            public ActionResult Edit(Movie movie, List<Guid> SelectedGenres, List<Guid> SelectedActors,
                List<Guid> SelectedDirectors)
            {
                if (ModelState.IsValid)
                {
                    var existingMovie = _db.Movies
                        .Include(m => m.Genres)
                        .Include(m => m.Actors)
                        .Include(m => m.Directors)
                        .FirstOrDefault(m => m.Id == movie.Id);

                    if (existingMovie == null) return HttpNotFound();

                    // Обновляем основные свойства
                    existingMovie.Title = movie.Title;
                    existingMovie.Year = movie.Year;
                    existingMovie.Country = movie.Country;
                    existingMovie.Description = movie.Description;
                    existingMovie.YouTubeVideoId = movie.YouTubeVideoId;

                    // Обновляем связи
                    UpdateMovieRelations(existingMovie, SelectedGenres, SelectedActors, SelectedDirectors);

                    _db.SaveChanges();
                    return RedirectToAction("Index");
                }

                return View(movie);
            }

            // GET: Movies/Delete/{id}
            public ActionResult Delete(Guid id)
            {
                var movie = _db.Movies.Find(id);
                if (movie == null) return HttpNotFound();
                return View(movie);
            }

            [HttpPost, ActionName("Delete")]
            [ValidateAntiForgeryToken]
            public ActionResult DeleteConfirmed(Guid id)
            {
                var movie = _db.Movies
                    .Include(m => m.Genres)
                    .Include(m => m.Actors)
                    .Include(m => m.Directors)
                    .FirstOrDefault(m => m.Id == id);

                if (movie != null)
                {
                    _db.Movies.Remove(movie);
                    _db.SaveChanges();
                }

                return RedirectToAction("Index");
            }

            private void UpdateMovieRelations(Movie movie, List<Guid> genres, List<Guid> actors, List<Guid> directors)
            {
                // Удаление старых связей
                _db.MovieGenres.RemoveRange(movie.Genres);
                _db.MovieActors.RemoveRange(movie.Actors);
                _db.MovieDirectors.RemoveRange(movie.Directors);

                // Добавление новых связей
                movie.Genres = genres?
                    .Select(g => new MovieGenre { GenreId = g })
                    .ToList() ?? new List<MovieGenre>();

                movie.Actors = actors?
                    .Select(a => new MovieActor { ActorId = a })
                    .ToList() ?? new List<MovieActor>();

                movie.Directors = directors?
                    .Select(d => new MovieDirector { DirectorId = d })
                    .ToList() ?? new List<MovieDirector>();

                _db.SaveChanges();
            }
        }
    }
