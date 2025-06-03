using System;
using ABSOLUTE_CINEMA.Domain.Entities;  

namespace ABSOLUTE_CINEMA.BusinessLogic.Interfaces
{

    public interface IUserSession
    {

        Guid GetCurrentUserId();


        UserRoleType GetCurrentUserRole();
    }
}