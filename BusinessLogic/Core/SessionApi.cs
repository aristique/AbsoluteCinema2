using System;
using System.Net.Sockets;
using System.Web;
using System.Web.Security;
using ABSOLUTE_CINEMA.BusinessLogic.Interfaces;
using ABSOLUTE_CINEMA.Domain.Entities;
using ABSOLUTE_CINEMA.Helpers;

namespace ABSOLUTE_CINEMA.BusinessLogic.Core
{
    public class SessionApi
    {
        public string DecryptUserData()
        {
            var cookie = HttpContext.Current?.Request?.Cookies[FormsAuthentication.FormsCookieName];
            if (cookie == null || string.IsNullOrEmpty(cookie.Value))
                throw new InvalidOperationException("Кука отсутствует или пуста.");

            var ticket = FormsAuthentication.Decrypt(cookie.Value);
            if (ticket == null || string.IsNullOrEmpty(ticket.UserData))
                throw new InvalidOperationException("Не удалось расшифровать билет аутентификации.");
                    
            return CookieGenerator.Validate(ticket.UserData);
        }
    }
}
