using System;
using System.Web;
using System.Web.Security;
using ABSOLUTE_CINEMA.BusinessLogic.Interfaces;
using ABSOLUTE_CINEMA.Domain.Entities;

namespace ABSOLUTE_CINEMA.BusinessLogic.Core
{
    /// <summary>
    /// Реализация IUserSession, извлекающая userId и роль из FormsAuthenticationTicket.
    /// </summary>
    public class SessionApi : IUserSession
    {
        public Guid GetCurrentUserId()
        {
            var ticket = GetAuthTicket();
            if (string.IsNullOrEmpty(ticket.UserData))
                throw new InvalidOperationException("Невозможно получить идентификатор пользователя.");

            var parts = ticket.UserData.Split('|');
            return Guid.Parse(parts[0]);
        }

        // Обновлённая сигнатура: возвращает UserRoleType
        public UserRoleType GetCurrentUserRole()
        {
            var ticket = GetAuthTicket();
            var parts = ticket.UserData.Split('|');
            if (parts.Length < 2 || string.IsNullOrEmpty(parts[1]))
                return UserRoleType.None;

            // в формате UserData: "userId|roleValue" где roleValue соответствует целочисленному значению UserRoleType
            if (Enum.TryParse<UserRoleType>(parts[1], out var roleType))
                return roleType;
            return UserRoleType.None;
        }

        private FormsAuthenticationTicket GetAuthTicket()
        {
            var cookieName = FormsAuthentication.FormsCookieName;
            var authCookie = HttpContext.Current.Request.Cookies[cookieName];
            if (authCookie == null || string.IsNullOrEmpty(authCookie.Value))
                throw new InvalidOperationException("Пользователь не аутентифицирован.");

            var ticket = FormsAuthentication.Decrypt(authCookie.Value);
            if (ticket == null)
                throw new InvalidOperationException("Невозможно получить данные аутентификации.");

            return ticket;
        }
    }
}