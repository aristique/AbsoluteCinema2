using System;
using System.Web.Mvc;
using ABSOLUTE_CINEMA.BusinessLogic.Interfaces;
using ABSOLUTE_CINEMA.BusinessLogic.Attributes;
using ABSOLUTE_CINEMA.BusinessLogic.BLogic;
using ABSOLUTE_CINEMA.Domain.DTO;

namespace ABSOLUTE_CINEMA.Controllers
{
    [CustomAuthorize]
    public class CommentsController : Controller
    {
        private readonly IComment _comment = new CommentBL();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Guid movieId, string text)
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                TempData["CommentError"] = "Комментарий не может быть пустым.";
                return RedirectToAction("Details", "Catalog", new { id = movieId });
            }

            var model = new CommentCreateModel
            {
                MovieId = movieId,
                Text = text
            };

            _comment.Create(model);
            return RedirectToAction("Details", "Catalog", new { id = movieId });
        }

        [CustomAuthorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Guid id, Guid movieId)
        {
            _comment.Delete(id);
            return RedirectToAction("Details", "Catalog", new { id = movieId });
        }
    }
}
