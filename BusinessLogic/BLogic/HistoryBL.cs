using System;
using System.Collections.Generic;
using System.Linq;
using ABSOLUTE_CINEMA.BusinessLogic.Core;
using ABSOLUTE_CINEMA.BusinessLogic.Interfaces;
using ABSOLUTE_CINEMA.Domain.DTO;
using ABSOLUTE_CINEMA.Domain.Entities;

namespace ABSOLUTE_CINEMA.BusinessLogic.BLogic
{
    public class HistoryBL : HistoryApi, IHistory
    {
        public void Create(ViewingHistoryDto dto)
        {
            if (dto == null || dto.MovieId == Guid.Empty)
                return;

            var session = new SessionBL();
            Guid userId = session.GetCurrentUserId();
            if (userId == Guid.Empty)
                return;

            var historyEntity = new ViewingHistory
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                MovieId = dto.MovieId,
                ViewedAt = DateTime.UtcNow
            };

            Add(historyEntity);
        }

        public void Delete(Guid id)
        {
            Remove(id);
        }

        public IEnumerable<ViewingHistoryDto> GetByUser(Guid userId, int limit = 0)
        {
            if (userId == Guid.Empty)
                return Enumerable.Empty<ViewingHistoryDto>();

            var allEntries = base.GetByUser(userId);
            var orderedEntries = allEntries.OrderByDescending(h => h.ViewedAt);
            if (limit > 0)
                orderedEntries = orderedEntries.Take(limit)
                                               .OrderByDescending(h => h.ViewedAt);

            return orderedEntries.Select(h =>
            {
                var movie = new CatalogBL().GetById(h.MovieId);
                return new ViewingHistoryDto
                {
                    Id = h.Id,
                    MovieId = h.MovieId,
                    ViewedAt = h.ViewedAt,
                    MovieTitle = movie?.Title,
                    YouTubeVideoId = movie?.YouTubeVideoId
                };
            })
            .ToList();
        }

        public void DeleteByUser(Guid userId)
        {
            if (userId == Guid.Empty)
                return;

            base.RemoveByUser(userId);
        }
    }
}
