using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using ABSOLUTE_CINEMA.BusinessLogic.Interfaces;
using ABSOLUTE_CINEMA.BusinessLogic.Core;
using ABSOLUTE_CINEMA.Domain.DTO;
using ABSOLUTE_CINEMA.Domain.Entities;

namespace ABSOLUTE_CINEMA.BusinessLogic.BLogic
{
    public class AccountBL : AccountApi, IAccount
    {
        private readonly WebDbContext _db;

        public AccountBL(WebDbContext db)
        {
            _db = db;
        }

        public UserRegistrationResponse Register(Register dto)
        {
            if (ExistsEmail(_db, dto.Email))
                return new UserRegistrationResponse { Success = false, Message = "Email уже используется" };

            var user = CreateUser(_db, dto);
            // При регистрации присваиваем роль User
            AssignRole(_db, user.Id, UserRoleType.User.ToString());
            return new UserRegistrationResponse { Success = true, UserId = user.Id };
        }

        // Возвращает роль текущего пользователя
        public UserRoleType Login(Login dto)
        {
            var user = FindByEmail(_db, dto.Email);
            if (user == null || !BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash))
                return UserRoleType.None;

            // Берём единственную роль из БД
            var roleName = GetRoles(_db, user.Id).FirstOrDefault();
            var role = Enum.TryParse<UserRoleType>(roleName, out var rt) ? rt : UserRoleType.None;

            // Сохраняем userId и роль в куки
            SignIn(user.Id, dto.Email, role);
            return role;
        }

        // Записывает FormsAuth-куку с UserData = "userId|roleValue"
        public void SignIn(Guid userId, string email, UserRoleType role)
        {
            var userData = $"{userId}|{(int)role}";
            var ticket = new FormsAuthenticationTicket(
                1,
                email,
                DateTime.Now,
                DateTime.Now.AddMinutes(30),
                false,
                userData
            );
            var encrypted = FormsAuthentication.Encrypt(ticket);
            var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encrypted)
            {
                HttpOnly = true
            };
            HttpContext.Current.Response.Cookies.Add(cookie);
        }

        public void SignOut()
        {
            FormsAuthentication.SignOut();
        }

        public List<UserViewModel> ListUsers() => ListUsers(_db);
    }
}
