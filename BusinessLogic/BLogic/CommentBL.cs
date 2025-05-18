using System;
using ABSOLUTE_CINEMA.BusinessLogic.Interfaces;
using ABSOLUTE_CINEMA.BusinessLogic.Core;
using ABSOLUTE_CINEMA.Domain.Entities;

namespace ABSOLUTE_CINEMA.BusinessLogic.BLogic
{
    public class CommentBL : CommentApi, IComment
    {
        private readonly WebDbContext _db;
        private readonly IUserSession _session;

        public CommentBL(WebDbContext db, IUserSession session)
        {
            _db = db;
            _session = session;
        }
        public void Create(Guid movieId, string text)
        {
            var userId = _session.GetCurrentUserId();
            var comment = new Comment
            {
                MovieId = movieId,
                UserId = userId,
                Text = text,
                CreatedAt = DateTime.UtcNow
            };
            Add(_db, comment);
        }

        public void Delete(Guid id)
        {
            Remove(_db, id);
        }
    }
}