﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using ABSOLUTE_CINEMA.AbsoluteCinema.ViewModels;
using ABSOLUTE_CINEMA.BusinessLogic.BLogic;
using ABSOLUTE_CINEMA.BusinessLogic.Interfaces;
using ABSOLUTE_CINEMA.Domain.DTO;
using ABSOLUTE_CINEMA.Domain.Entities;

namespace ABSOLUTE_CINEMA.Controllers
{
    public class CatalogController : Controller
    {
        private readonly ICatalog _catalog = new CatalogBL();
        private readonly IHistory _historyService = new HistoryBL();
        private readonly IUserSession _session = new SessionBL();

        [HttpGet]
        public ActionResult Index(List<string> genres)
        {
  
            var subscriptionService = new SubscriptionBL();
            bool hasPaidSubscription = subscriptionService.HasActive();
            ViewBag.HasPaidSubscription = hasPaidSubscription;

            var movies = _catalog.GetAll();
            if (genres != null && genres.Any())
            {
                movies = movies
                    .Where(m => m.Genres != null && m.Genres.Any(g => genres.Contains(g.Name)))
                    .ToList();
            }

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

            var allGenres = _catalog
                .GetGenres()
                .Select(g => new GenreViewModel
                {
                    Id = g.Id,
                    Name = g.Name
                })
                .ToList();

            if (!hasPaidSubscription)
            {
                allGenres = allGenres
                    .Where(g => !string.Equals(g.Name, "Платное", StringComparison.OrdinalIgnoreCase))
                    .ToList();
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

            Guid userId = _session.GetCurrentUserId();
            if (userId != Guid.Empty)
            {
               
                var dto = new ViewingHistoryDto
                {
                    MovieId = id
                                    };
                _historyService.Create(dto);
            }

           
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

        
        [HttpGet]
        [Authorize]
        public ActionResult MyHistory(int limit = 50)
        {
          
            Guid userId = _session.GetCurrentUserId();
            if (userId == Guid.Empty)
                return RedirectToAction("Login", "Account");

      
            var historyDtos = _historyService.GetByUser(userId, limit);

     
            var vm = historyDtos.Select(h => new HistoryItemViewModel
            {
                MovieId = h.MovieId,
                MovieTitle = h.MovieTitle,
                YouTubeVideoId = h.YouTubeVideoId,
                ViewedAt = h.ViewedAt
            })
            .ToList();

            return View(vm);
        }
    }
}
