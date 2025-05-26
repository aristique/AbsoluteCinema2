using System;
using System.Web.Mvc;
using ABSOLUTE_CINEMA.BusinessLogic.BLogic;

namespace ABSOLUTE_CINEMA.Controllers
{
    [Authorize]
    public class CommentsController : Controller
    {
        private readonly CommentBL _comment = new CommentBL();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Guid movieId, string text)
        {
            // Проверка на пустой комментарий
            if (string.IsNullOrWhiteSpace(text))
            {
                TempData["CommentError"] = "Комментарий не может быть пустым.";
                return RedirectToAction("Details", "Catalog", new { id = movieId });
            }

            // Добавление комментария
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
