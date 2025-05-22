// Global.asax.cs
using System;
using System.Linq;
using System.Collections.Generic;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using System.Web.Helpers;              // Для AntiForgeryConfig

// Сущности домена
using ABSOLUTE_CINEMA.Domain.Entities;
// Контекст базы данных
using ABSOLUTE_CINEMA;

namespace ABSOLUTE_CINEMA
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);

            // Отключаем жёсткую проверку привязки анти-фальшивого токена к имени пользователя
            AntiForgeryConfig.SuppressIdentityHeuristicChecks = true;

            // Настройка DI-контейнера
            UnityConfig.RegisterComponents();

            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            // Инициализация ролей и начальных данных
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
                    {
                        if (!db.Roles.Any(r => r.Name == name))
                            db.Roles.Add(new Role { Name = name });
                    }
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
                        new Genre { Name = "Боевик" },
                        new Genre { Name = "Драма" },
                        new Genre { Name = "Комедия" }
                    });
                }
                if (!db.Actors.Any())
                {
                    db.Actors.AddRange(new[]
                    {
                        new Actor { Name = "Том Круз" },
                        new Actor { Name = "Марго Робби" }
                    });
                }
                if (!db.Directors.Any())
                {
                    db.Directors.AddRange(new[]
                    {
                        new Director { Name = "Квентин Тарантино" },
                        new Director { Name = "Кристофер Нолан" }
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
                var identity = new GenericIdentity(ticket.Name, "Forms");
                var roles = ticket.UserData.Split(new[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
                HttpContext.Current.User = new GenericPrincipal(identity, roles);
            }
            catch
            {
                // Ошибка дешифровки — пропускаем аутентификацию
            }
        }
    }
}
