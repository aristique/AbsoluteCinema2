using System;
using System;
using ABSOLUTE_CINEMA.BusinessLogic.Interfaces;
using ABSOLUTE_CINEMA.BusinessLogic.Core;
using ABSOLUTE_CINEMA.Domain.Entities;

namespace ABSOLUTE_CINEMA.BusinessLogic.BLogic
{
        private readonly WebDbContext _db;
        private readonly IUserSession _session;
    {
        _db = db;
        _session = session;
    }
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
