using System;
using System.Web.Mvc;
using ABSOLUTE_CINEMA.BusinessLogic.Interfaces;
using ABSOLUTE_CINEMA.Domain.Entities;
using System.Linq;
using System.Web.Mvc;

namespace ABSOLUTE_CINEMA.Controllers
{
    public class CatalogController : Controller
    {
        private readonly ICatalog _catalog;

        public CatalogController(ICatalog catalog)
        {
            _catalog = catalog;
        }

        public ActionResult Index(string genre = null)
        {
            var movies = _catalog.GetAll(genre);
            ViewBag.Genres = _catalog.GetGenres();
            return View(movies);
        }

        public ActionResult Details(Guid id)
        {
            var movie = _catalog.GetById(id);
            if (movie == null) return HttpNotFound();

            ViewBag.Genres = _catalog.GetGenres();
            return View(movie);
        }
    }
}