using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using ABSOLUTE_CINEMA.Models;

namespace ABSOLUTE_CINEMA
{
    public class MvcApplication : System.Web.HttpApplication
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
            {
                using (var transaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        // Создание ролей
                        var requiredRoles = new[] { "Admin", "RegisteredUser", "Guest" };
                        foreach (var roleName in requiredRoles)
                        {
                            if (!db.Roles.Any(r => r.Name == roleName))
                            {
                                db.Roles.Add(new Role { Name = roleName });
                            }
                        }
                        db.SaveChanges();

                        // Создание администратора
                        var adminRole = db.Roles.First(r => r.Name == "Admin");
                        if (!db.Users.Any(u => u.Email == "admin@cinema.com"))
                        {
                            var admin = new User
                            {
                                Id = Guid.NewGuid(), // Явное указание нового GUID
                                Name = "Admin",
                                Email = "admin@cinema.com",
                                PasswordHash = BCrypt.Net.BCrypt.HashPassword("StrongAdminPassword123!")
                            };

                            db.Users.Add(admin);
                            db.SaveChanges(); // Сохраняем пользователя сначала

                            db.UserRoles.Add(new UserRole
                            {
                                UserId = admin.Id,
                                RoleId = adminRole.Id
                            });
                            db.SaveChanges();
                        }

                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        // Логирование ошибки
                        System.Diagnostics.Debug.WriteLine($"Initialization error: {ex.Message}");
                        throw;
                    }
                }
            }
        }
        // В Global.asax.cs
        private void EnsureInitialData()
        {
            using (var db = new WebDbContext())
            {
                // Жанры
                if (!db.Genres.Any())
                {
                    var genres = new List<Genre>
                    {
                        new Genre { Name = "Боевик" },
                        new Genre { Name = "Драма" },
                        new Genre { Name = "Комедия" }
                    };
                    db.Genres.AddRange(genres);
                }

                // Актеры
                if (!db.Actors.Any())
                {
                    var actors = new List<Actor>
                    {
                        new Actor { Name = "Том Круз" },
                        new Actor { Name = "Марго Робби" }
                    };
                    db.Actors.AddRange(actors);
                }

                // Режиссеры
                if (!db.Directors.Any())
                {
                    var directors = new List<Director>
                    {
                        new Director { Name = "Квентин Тарантино" },
                        new Director { Name = "Кристофер Нолан" }
                    };
                    db.Directors.AddRange(directors);
                }

                db.SaveChanges();
            }
        }
        
        protected void Application_AuthenticateRequest(Object sender, EventArgs e)
        {
            HttpCookie authCookie = Request.Cookies[FormsAuthentication.FormsCookieName];
            if (authCookie != null)
            {
                try
                {
                    FormsAuthenticationTicket authTicket = FormsAuthentication.Decrypt(authCookie.Value);
                    var identity = new GenericIdentity(authTicket.Name, "Forms");
            
                    // Десериализуем роли из UserData
                    var roles = authTicket.UserData.Split(new[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
            
                    var principal = new GenericPrincipal(identity, roles);
                    HttpContext.Current.User = principal;
                }
                catch (Exception ex)
                {
                    // Логирование ошибки
                    System.Diagnostics.Debug.WriteLine($"Authentication error: {ex.Message}");
                }
            }
        }
    }
}