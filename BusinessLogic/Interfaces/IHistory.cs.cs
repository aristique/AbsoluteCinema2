// BusinessLogic/Interfaces/IHistory.cs
using System;
using System.Collections.Generic;
using ABSOLUTE_CINEMA.Domain.DTO;

namespace ABSOLUTE_CINEMA.BusinessLogic.Interfaces
{
    public interface IHistory
    {
        void Create(ViewingHistoryDto dto);
        void Delete(Guid id);
        IEnumerable<ViewingHistoryDto> GetByUser(Guid userId, int limit = 0);
        void DeleteByUser(Guid userId);
    }
}
