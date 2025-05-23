﻿using System;
using System.Web.Mvc;
using ABSOLUTE_CINEMA.BusinessLogic.Interfaces;

namespace ABSOLUTE_CINEMA.Controllers
{
    [Authorize]
    public class CommentsController : Controller
    {
        private readonly IComment _comment;

        public CommentsController(IComment comment)
        {
            _comment = comment;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Guid movieId, string text)
        {
            if (!ModelState.IsValid)
                return RedirectToAction("Details", "Catalog", new { id = movieId });

            _comment.Create(movieId, text);
            return RedirectToAction("Details", "Catalog", new { id = movieId });
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Guid id, Guid movieId)
        {
            _comment.Delete(id);
            return RedirectToAction("Details", "Catalog", new { id = movieId });
        }
    }
}