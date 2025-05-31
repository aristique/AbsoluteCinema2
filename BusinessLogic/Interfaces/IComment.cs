using System;
using ABSOLUTE_CINEMA.Domain.DTO;

namespace ABSOLUTE_CINEMA.BusinessLogic.Interfaces
{
    public interface IComment
    {
        void Create(CommentCreateModel model);
        void Delete(Guid id);
    }
}
