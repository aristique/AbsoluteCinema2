using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using ABSOLUTE_CINEMA.AbsoluteCinema.ViewModels;
using ABSOLUTE_CINEMA.BusinessLogic.Interfaces;
using ABSOLUTE_CINEMA.BusinessLogic.BLogic;

namespace ABSOLUTE_CINEMA.Controllers
{
    public class HomeController : Controller
    {
       
        private readonly ICatalog _catalog = new CatalogBL();

        [HttpGet]
        public ActionResult Index()
        {
            
            var popularMovies = _catalog
                .GetPopular(4)
                .Select(m => new MovieViewModel
                {
                    Id = m.Id,
                    Title = m.Title,
                    Year = m.Year,
                    YouTubeVideoId = m.YouTubeVideoId
                })
                .ToList();

           
            return View(popularMovies);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
            return View();
        }
    }
}
