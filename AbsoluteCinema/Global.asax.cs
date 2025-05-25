using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using ABSOLUTE_CINEMA.Domain.Entities;

namespace ABSOLUTE_CINEMA
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            EnsureRolesAndAdminCreated();
            EnsureInitialData();
        }

        private void EnsureRolesAndAdminCreated()
        {
            using (var db = new WebDbContext())
            using (var tx = db.Database.BeginTransaction())
            {
                try
                {
                    var required = new[] { "Admin", "RegisteredUser", "Guest" };
                    foreach (var name in required)
                        if (!db.Roles.Any(r => r.Name == name))
                            db.Roles.Add(new Role { Name = name });

                    db.SaveChanges();

                    var adminRole = db.Roles.First(r => r.Name == "Admin");
                    if (!db.Users.Any(u => u.Email == "admin@cinema.com"))
                    {
                        var admin = new User
                        {
                            Id = Guid.NewGuid(),
                            Name = "Administrator",
                            Email = "admin@cinema.com",
                            PasswordHash = BCrypt.Net.BCrypt.HashPassword("StrongAdminPassword123!")
                        };
                        db.Users.Add(admin);
                        db.SaveChanges();

                        db.UserRoles.Add(new UserRole
                        {
                            UserId = admin.Id,
                            RoleId = adminRole.Id
                        });
                        db.SaveChanges();
                    }

                    tx.Commit();
                }
                catch
                {
                    tx.Rollback();
                    throw;
                }
            }
        }

        private void EnsureInitialData()
        {
            using (var db = new WebDbContext())
            {
                if (!db.Genres.Any())
                {
                    db.Genres.AddRange(new[]
                    {
                        new Genre { Id = Guid.NewGuid(), Name = "Боевик" },
                        new Genre { Id = Guid.NewGuid(), Name = "Драма" },
                        new Genre { Id = Guid.NewGuid(), Name = "Комедия" }
                    });
                }

                if (!db.Actors.Any())
                {
                    db.Actors.AddRange(new[]
                    {
                        new Actor { Id = Guid.NewGuid(), Name = "Том Круз" },
                        new Actor { Id = Guid.NewGuid(), Name = "Марго Робби" }
                    });
                }

                if (!db.Directors.Any())
                {
                    db.Directors.AddRange(new[]
                    {
                        new Director { Id = Guid.NewGuid(), Name = "Квентин Тарантино" },
                        new Director { Id = Guid.NewGuid(), Name = "Кристофер Нолан" }
                    });
                }

                db.SaveChanges();
            }
        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {
            var authCookie = Request.Cookies[FormsAuthentication.FormsCookieName];
            if (authCookie == null) return;

            try
            {
                var ticket = FormsAuthentication.Decrypt(authCookie.Value);
                var decryptedUserData = ABSOLUTE_CINEMA.Helpers.CookieGenerator.Validate(ticket.UserData);

                var parts = decryptedUserData.Split('|');
                var identity = new System.Security.Principal.GenericIdentity(ticket.Name, "Forms");

                
                var roles = parts.Length >= 2 ? new[] { parts[1] } : new string[0];

                HttpContext.Current.User = new System.Security.Principal.GenericPrincipal(identity, roles);
            }
            catch
            {
         
            }
        }

    }
}
