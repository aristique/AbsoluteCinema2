using System;
using ABSOLUTE_CINEMA.Domain.Entities;  // импорт перечисления UserRole

namespace ABSOLUTE_CINEMA.BusinessLogic.Interfaces
{

    public interface IUserSession
    {

        Guid GetCurrentUserId();


        UserRoleType GetCurrentUserRole();
    }
}