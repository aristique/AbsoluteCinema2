using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ABSOLUTE_CINEMA.BusinessLogic.BLogic;
using ABSOLUTE_CINEMA.Domain.Entities;

namespace ABSOLUTE_CINEMA.BusinessLogic.Attributes
{
    public class CustomAuthorizeAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// Список разрешённых ролей через запятую (например "Admin,User").
        /// Оставьте пустым, чтобы пропустить проверку ролей.
        /// </summary>
        public string Roles { get; set; }

        public CustomAuthorizeAttribute() { }

        public CustomAuthorizeAttribute(string roles)
        {
            Roles = roles;
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var http = filterContext.HttpContext;
            var sessionBl = new SessionBL(); // BL создаётся здесь

            // 1) Проверяем факт логина
            var userId = sessionBl.GetCurrentUserId();
            if (userId == Guid.Empty)
            {
                var returnUrl = HttpUtility.UrlEncode(http.Request.RawUrl);
                filterContext.Result = new RedirectResult($"/Account/Login?returnUrl={returnUrl}");
                return;
            }

            // 2) Проверяем роли, если нужно
            if (!string.IsNullOrWhiteSpace(Roles))
            {
                var currentRole = sessionBl.GetCurrentUserRole().ToString();
                var allowed = Roles
                    .Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(r => r.Trim());

                if (!allowed.Any(r => string.Equals(r, currentRole, StringComparison.OrdinalIgnoreCase)))
                {
                    filterContext.Result = new RedirectResult("/Error/403");
                    return;
                }
            }

            base.OnActionExecuting(filterContext);
        }
    }
}
