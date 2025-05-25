using System;
using System.Web.Mvc;
using ABSOLUTE_CINEMA.BusinessLogic.BLogic;

namespace ABSOLUTE_CINEMA.Controllers
{
    public class CatalogController : Controller
    {
        private readonly CatalogBL _catalog = new CatalogBL();

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
