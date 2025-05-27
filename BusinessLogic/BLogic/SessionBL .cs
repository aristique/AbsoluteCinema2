using System;
using ABSOLUTE_CINEMA.BusinessLogic.Interfaces;
using ABSOLUTE_CINEMA.BusinessLogic.Core;
using ABSOLUTE_CINEMA.Domain.Entities;

namespace ABSOLUTE_CINEMA.BusinessLogic.BLogic
{
    public class SessionBL : SessionApi, IUserSession
    {
        public Guid GetCurrentUserId()
        {
            var data = DecryptUserData();            
            var parts = data.Split('|');
            if (!Guid.TryParse(parts[0], out var id))
                throw new InvalidOperationException("Некорректный ID пользователя");
            return id;
        }

        public UserRoleType GetCurrentUserRole()
        {
            var data = DecryptUserData();
            var parts = data.Split('|');

            
            if (parts.Length < 3)
                return UserRoleType.None;

           
            return Enum.TryParse<UserRoleType>(parts[2], out var role)
                ? role
                : UserRoleType.None;
        }
    }
}
