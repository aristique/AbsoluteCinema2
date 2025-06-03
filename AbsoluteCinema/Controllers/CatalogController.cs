using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using ABSOLUTE_CINEMA.AbsoluteCinema.ViewModels;
using ABSOLUTE_CINEMA.BusinessLogic.BLogic;
using ABSOLUTE_CINEMA.BusinessLogic.Interfaces;

namespace ABSOLUTE_CINEMA.Controllers
{
    public class CatalogController : Controller
    {
        private readonly ICatalog _catalog = new CatalogBL();

        [HttpGet]
        public ActionResult Index(List<string> genres)
        {
            // 1. Проверяем, есть ли у текущего пользователя активная платная подписка:
            var subscriptionService = new SubscriptionBL();
            bool hasPaidSubscription = subscriptionService.HasActive();
            ViewBag.HasPaidSubscription = hasPaidSubscription;

            // 2. Получаем все фильмы
            var movies = _catalog.GetAll();

            // 3. Применяем фильтр по выбранным жанрам, если он задан
            if (genres != null && genres.Any())
            {
                movies = movies
                    .Where(m => m.Genres != null && m.Genres.Any(g => genres.Contains(g.Name)))
                    .ToList();
            }

            // 4. Маппим фильмы в MovieViewModel, включая список жанров
            var viewModels = movies
                .Select(m => new MovieViewModel
                {
                    Id = m.Id,
                    Title = m.Title,
                    Year = m.Year,
                    YouTubeVideoId = m.YouTubeVideoId,
                    Genres = m.Genres
                              .Select(g => new GenreViewModel
                              {
                                  Id = g.Id,
                                  Name = g.Name
                              })
                              .ToList()
                })
                .ToList();

            // 5. Получаем полный список жанров для фильтрации
            var allGenres = _catalog
                .GetGenres()
                .Select(g => new GenreViewModel
                {
                    Id = g.Id,
                    Name = g.Name
                })
                .ToList();

            // 6. Если нет платной подписки, удаляем жанр "Платное" из списка
            if (!hasPaidSubscription)
            {
                allGenres = allGenres
                    .Where(g => !string.Equals(g.Name, "Платное", StringComparison.OrdinalIgnoreCase))
                    .ToList();

                // Убираем "Платное" из уже выбранных жанров, если оно там есть
                genres = genres?
                    .Where(g => !string.Equals(g, "Платное", StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }

            ViewBag.Genres = allGenres;
            ViewBag.SelectedGenres = genres ?? new List<string>();

            return View(viewModels);
        }

        [HttpGet]
        public ActionResult TrackAndDetails(Guid id)
        {
            _catalog.IncrementDetailsViewCount(id);
            return RedirectToAction("Details", new { id });
        }

        [HttpGet]
        public ActionResult Details(Guid id)
        {
            var movie = _catalog.GetById(id);
            if (movie == null)
                return HttpNotFound();

            var model = new MovieDetailsViewModel
            {
                Id = movie.Id,
                Title = movie.Title,
                Year = movie.Year,
                Country = movie.Country,
                Description = movie.Description,
                YouTubeVideoId = movie.YouTubeVideoId,
                Genres = movie.Genres.ToList(),
                Actors = movie.Actors.ToList(),
                Directors = movie.Directors.ToList(),
                Comments = movie.Comments
                    .OrderByDescending(c => c.CreatedAt)
                    .Select(c => new CommentViewModel
                    {
                        Id = c.Id,
                        Text = c.Text,
                        UserName = c.UserName,
                        CreatedAt = c.CreatedAt
                    })
                    .ToList()
            };

            return View(model);
        }
    }
}
