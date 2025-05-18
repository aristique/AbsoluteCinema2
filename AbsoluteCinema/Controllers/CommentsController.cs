using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;
using ABSOLUTE_CINEMA.Domain.Entities;
using ABSOLUTE_CINEMA.Domain.DTO;

namespace ABSOLUTE_CINEMA.Controllers
{
    [Authorize]
    public class CommentsController : Controller
    {
        private readonly WebDbContext _db = new WebDbContext();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Guid movieId, string text)
        {
            try
            {
                // Получаем текущего пользователя
                var userEmail = User.Identity.Name;
                var user = _db.Users.FirstOrDefault(u => u.Email == userEmail);
                
                if (user == null)
                {
                    ModelState.AddModelError("", "Пользователь не найден");
                    return RedirectToAction("Details", "Catalog", new { id = movieId });
                }

                if (string.IsNullOrWhiteSpace(text))
                {
                    ModelState.AddModelError("", "Текст комментария не может быть пустым");
                    return RedirectToAction("Details", "Catalog", new { id = movieId });
                }

                var comment = new Comment
                {
                    MovieId = movieId,
                    UserId = user.Id,
                    Text = text
                };

                _db.Comments.Add(comment);
                _db.SaveChanges();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Ошибка: {ex.Message}");
            }
            
            return RedirectToAction("Details", "Catalog", new { id = movieId });
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Guid id)
        {
            try
            {
                var comment = _db.Comments.Find(id);
                if (comment != null)
                {
                    var movieId = comment.MovieId;
                    _db.Comments.Remove(comment);
                    _db.SaveChanges();
                    return RedirectToAction("Details", "Catalog", new { id = movieId });
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Ошибка: {ex.Message}");
            }
            return RedirectToAction("Index", "Catalog");
        }
    }
}