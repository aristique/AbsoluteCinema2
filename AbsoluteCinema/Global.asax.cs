using System;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using ABSOLUTE_CINEMA.Domain.Entities;
using ABSOLUTE_CINEMA.Helpers;

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
            var authCookie = Context.Request.Cookies["AuthCookie"];
            if (authCookie == null || string.IsNullOrEmpty(authCookie.Value))
                return;

            try
            {
                
                var decrypted = CookieGenerator.Validate(authCookie.Value);
                var parts = decrypted.Split('|');
                if (parts.Length != 4) return;

                
                if (!DateTime.TryParse(parts[3], null,
                        System.Globalization.DateTimeStyles.RoundtripKind,
                        out var expireUtc)
                    || expireUtc < DateTime.UtcNow)
                    return;

                
                var email = parts[1];
                var role = parts[2];

                
                var identity = new GenericIdentity(email, "CustomCookie");
                var principal = new GenericPrincipal(identity, new[] { role });

                
                Context.User = principal;
                System.Threading.Thread.CurrentPrincipal = principal;
            }
            catch
            {
                
            }
        }

    }
}
