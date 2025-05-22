using System;
using ABSOLUTE_CINEMA.BusinessLogic.Interfaces;
using ABSOLUTE_CINEMA.BusinessLogic.Core;
using ABSOLUTE_CINEMA.Domain.Entities;

namespace ABSOLUTE_CINEMA.BusinessLogic.BLogic
{
    /// <summary>
    /// Сервис для работы с комментариями: добавление и удаление.
    /// </summary>
    public class CommentBL : CommentApi, IComment
    {
        private readonly WebDbContext _db;
        private readonly IUserSession _session;

        public CommentBL(WebDbContext db, IUserSession session)
        {
            _db = db;
            _session = session;
        }

        /// <summary>
        /// Создает комментарий к фильму от текущего пользователя.
        /// </summary>
        public void Create(Guid movieId, string text)
        {
            var userId = _session.GetCurrentUserId();
            var comment = new Comment
            {
                Id = Guid.NewGuid(),
                MovieId = movieId,
                UserId = userId,
                Text = text,
                CreatedAt = DateTime.UtcNow
            };
            // Базовый метод CommentApi.Add
            base.Add(_db, comment);
        }

        /// <summary>
        /// Удаляет комментарий по идентификатору.
        /// </summary>
        public void Delete(Guid id)
        {
            // Базовый метод CommentApi.Remove
            base.Remove(_db, id);
        }
    }
}
