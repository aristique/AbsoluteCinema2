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
            return Guid.TryParse(parts[0], out var id) ? id : throw new InvalidOperationException("Некорректный ID пользователя");
        }

        public UserRoleType GetCurrentUserRole()
        {
            var data = DecryptUserData();
            var parts = data.Split('|');
            if (parts.Length < 2) return UserRoleType.None;

            return Enum.TryParse<UserRoleType>(parts[1], out var role) ? role : UserRoleType.None;
        }
    }
}
