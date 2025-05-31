using System;
using ABSOLUTE_CINEMA.BusinessLogic.Core;
using ABSOLUTE_CINEMA.BusinessLogic.Interfaces;
using ABSOLUTE_CINEMA.Domain.DTO;
using ABSOLUTE_CINEMA.Domain.Entities;

namespace ABSOLUTE_CINEMA.BusinessLogic.BLogic
{
    public class CommentBL : CommentApi, IComment
    {
        public void Create(CommentCreateModel model)
        {
            var session = new SessionBL();
            var userId = session.GetCurrentUserId();

            var comment = new Comment
            {
                Id = Guid.NewGuid(),
                MovieId = model.MovieId,
                UserId = userId,
                Text = model.Text,
                CreatedAt = DateTime.UtcNow
            };

            Addd(comment);
        }

        public void Delete(Guid id)
        {
            Removee(id);
        }
    }
}
