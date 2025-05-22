// App_Start/UnityConfig.cs
using System.Web.Mvc;
using Unity;
using Unity.Lifetime;
using Unity.Mvc5;

using ABSOLUTE_CINEMA.BusinessLogic.Interfaces;
using ABSOLUTE_CINEMA.BusinessLogic.BLogic;
using ABSOLUTE_CINEMA.BusinessLogic.Core;
using ABSOLUTE_CINEMA.BusinessLogic;
using System.Data.Entity.Core.Metadata.Edm;

namespace ABSOLUTE_CINEMA
{
    /// <summary>
    /// Настройка контейнера внедрения зависимостей Unity
    /// </summary>
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
            var container = new UnityContainer();

            // Регистрация контекста БД per-request
            container.RegisterType<WebDbContext>(new HierarchicalLifetimeManager());

            // Сессия пользователя (извлечение userId/role из куки)
            container.RegisterType<IUserSession, SessionApi>(new HierarchicalLifetimeManager());

            // Account
            container.RegisterType<IAccount, AccountBL>(new HierarchicalLifetimeManager());

            // Comments
            container.RegisterType<IComment, CommentBL>(new HierarchicalLifetimeManager());

            // Catalog
            container.RegisterType<ICatalog, CatalogBL>(new HierarchicalLifetimeManager());

            // Movies
            container.RegisterType<IMovie, MovieBL>(new HierarchicalLifetimeManager());

            // Profile
            container.RegisterType<IProfile, ProfileBL>(new HierarchicalLifetimeManager());

            // Subscription
            container.RegisterType<ISubscription, SubscriptionBL>(new HierarchicalLifetimeManager());

            // UserList (админ)
            container.RegisterType<IUserList, UserListBL>(new HierarchicalLifetimeManager());

            // AdminService
            container.RegisterType<IAdmin, AdminBL>(new HierarchicalLifetimeManager());

            // Устанавливаем резолвер для MVC
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}
