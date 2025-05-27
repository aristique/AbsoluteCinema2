using System;
using System.Web.Mvc;
using ABSOLUTE_CINEMA.BusinessLogic.BLogic;
using ABSOLUTE_CINEMA.BusinessLogic.Attributes;

namespace ABSOLUTE_CINEMA.Controllers
{
    [CustomAuthorize]  
    public class CommentsController : Controller
    {
        private readonly CommentBL _comment = new CommentBL();

        [HttpPost]
        public ActionResult Create(Guid movieId, string text)
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                TempData["CommentError"] = "Комментарий не может быть пустым.";
                return RedirectToAction("Details", "Catalog", new { id = movieId });
            }

            _comment.Create(movieId, text);
            return RedirectToAction("Details", "Catalog", new { id = movieId });
        }

        [CustomAuthorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult Delete(Guid id, Guid movieId)
        {
            _comment.Delete(id);
            return RedirectToAction("Details", "Catalog", new { id = movieId });
        }
    }
}
