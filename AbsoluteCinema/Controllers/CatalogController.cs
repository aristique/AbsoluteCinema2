using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using ABSOLUTE_CINEMA.Domain.Entities;
using ABSOLUTE_CINEMA.Domain.DTO;

namespace ABSOLUTE_CINEMA.Controllers
{
    public class CatalogController : Controller
    {
        private readonly WebDbContext _db = new WebDbContext();

        public ActionResult Index(string genre = null)
        {
            try
            {
                IQueryable<Movie> query = _db.Movies
                    .Include(m => m.Genres.Select(g => g.Genre));

                if (!string.IsNullOrEmpty(genre))
                {
                    query = query.Where(m => m.Genres.Any(g => g.Genre.Name == genre));
                }

                var movies = query.ToList();
                ViewBag.Genres = _db.Genres.ToList(); // Всегда инициализируем

                return View(movies);
            }
            catch (Exception ex)
            {
                // Логирование ошибки
                return View("Error");
            }
        }
        // CatalogController.cs
        public ActionResult ByGenre(string genre)
        {
            var movies = _db.Movies
                .Include(m => m.Genres.Select(g => g.Genre))
                .Where(m => m.Genres.Any(g => g.Genre.Name == genre))
                .ToList();

            ViewBag.GenreName = genre;
            ViewBag.Genres = _db.Genres.ToList(); // Добавлено

            return View("Index", movies);
        }

        public ActionResult Details(Guid id)
        {
            var movie = _db.Movies
                .Include(m => m.Genres.Select(g => g.Genre))
                .Include(m => m.Actors.Select(a => a.Actor))
                .Include(m => m.Directors.Select(d => d.Director))
                .FirstOrDefault(m => m.Id == id);

            if (movie == null)
            {
                return HttpNotFound();
            }

            ViewBag.Genres = _db.Genres.ToList(); // Всегда инициализируем

            return View(movie);
        }

        // Остальные методы без изменений
    }
}