using System;
using System.Web;
using ABSOLUTE_CINEMA.Helpers;

namespace ABSOLUTE_CINEMA.BusinessLogic.Core
{
    public class SessionApi
    {
        private const string CookieName = "AuthCookie";


        public string DecryptUserData()
        {
            var http = HttpContext.Current;
            if (http == null)
                throw new InvalidOperationException("Нет текущего HttpContext.");

            var cookie = http.Request.Cookies[CookieName];
            if (cookie == null || string.IsNullOrEmpty(cookie.Value))
                throw new InvalidOperationException("AuthCookie отсутствует или пуста.");

            
            var decrypted = CookieGenerator.Validate(cookie.Value);
            if (string.IsNullOrEmpty(decrypted))
                throw new InvalidOperationException("Не удалось расшифровать AuthCookie.");

            return decrypted;
        }

        
        public Guid GetCurrentUserId()
        {
            var data = DecryptUserData();            
            var parts = data.Split('|');
            if (parts.Length < 1)
                throw new InvalidOperationException("Неправильный формат данных в AuthCookie.");

            if (!Guid.TryParse(parts[0], out var userId))
                throw new InvalidOperationException("Не удалось распознать userId в AuthCookie.");

            return userId;
        }


        public string GetCurrentUserRole()
        {
            var data = DecryptUserData();
            var parts = data.Split('|');
            if (parts.Length < 3)
                throw new InvalidOperationException("Неправильный формат данных в AuthCookie.");

            return parts[2]; 
        }
    }
}
