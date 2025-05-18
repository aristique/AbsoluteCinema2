using System;

namespace ABSOLUTE_CINEMA.BusinessLogic.Interfaces
{
    public interface IComment
    {
        void Create(Guid movieId, string text);
        void Delete(Guid id);
    }
}