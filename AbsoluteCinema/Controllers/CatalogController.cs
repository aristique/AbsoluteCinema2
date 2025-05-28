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

        public ActionResult Index(List<string> genres = null)
        {
            var movies = _catalog.GetAll();

            
            if (genres != null && genres.Any())
            {
                movies = movies
                    .Where(m => m.Genres.Any(g => genres.Contains(g.Genre.Name)))
                    .ToList();
            }

            
            var viewModels = movies.Select(m => new MovieViewModel
            {
                Id = m.Id,
                Title = m.Title,
                Year = m.Year,
                YouTubeVideoId = m.YouTubeVideoId
            }).ToList();

           
            var allGenres = _catalog.GetGenres()
                .Select(g => new GenreViewModel
                {
                    Id = g.Id,
                    Name = g.Name
                }).ToList();

            ViewBag.Genres = allGenres;
            ViewBag.SelectedGenres = genres ?? new List<string>();

            return View(viewModels);
        }

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
                Genres = movie.Genres.Select(g => g.Genre.Name).ToList(),
                Actors = movie.Actors.Select(a => a.Actor.Name).ToList(),
                Directors = movie.Directors.Select(d => d.Director.Name).ToList(),
                Comments = movie.Comments.Select(c => new CommentViewModel
                {
                    Id = c.Id,
                    Text = c.Text,
                    UserName = c.User?.Name ,
                    CreatedAt = c.CreatedAt
                }).ToList()
            };

            return View(model);
        }

    }
}
