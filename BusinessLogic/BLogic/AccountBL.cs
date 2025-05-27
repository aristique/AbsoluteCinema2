using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ABSOLUTE_CINEMA.BusinessLogic.Interfaces;
using ABSOLUTE_CINEMA.BusinessLogic.Core;
using ABSOLUTE_CINEMA.Domain.DTO;
using ABSOLUTE_CINEMA.Domain.Entities;
using ABSOLUTE_CINEMA.Helpers;     

namespace ABSOLUTE_CINEMA.BusinessLogic.BLogic
{
    public class AccountBL : AccountApi, IAccount
    {
        private const string CookieName = "AuthCookie";
        private static readonly TimeSpan CookieLifetime = TimeSpan.FromMinutes(30);

       
        public UserRegistrationResponse Register(Registerr dto)
        {
            if (ExistsEmail(dto.Email))
                return new UserRegistrationResponse { Success = false, Message = "Email уже используется" };

            var user = new User
            {
                Id = Guid.NewGuid(),
                Name = dto.Name,
                Email = dto.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password),
                CreatedAt = DateTime.UtcNow
            };

            var createdUser = CreateUser(user);

            
            AssignRole(createdUser.Id, "RegisteredUser");

            return new UserRegistrationResponse
            {
                Success = true,
                UserId = createdUser.Id
            };
        }

        public Guid GetUserIdByEmail(string email)
        {
            var user = FindByEmail(email);
            return user?.Id ?? Guid.Empty;
        }

        public UserRoleType Login(Login dto)
        {
            var user = FindByEmail(dto.Email);
            if (user == null || !BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash))
                return UserRoleType.None;

            var roleName = GetRoles(user.Id).FirstOrDefault();
            if (roleName == "Banned" || roleName == "Guest")
                return UserRoleType.None;

            var role = Enum.TryParse<UserRoleType>(roleName, out var rt)
                ? rt
                : UserRoleType.None;

            
            SignIn(user.Id, user.Email, role);
            return role;
        }

        public void SignIn(Guid userId, string email, UserRoleType role)
        {
            var expireUtc = DateTime.UtcNow.Add(CookieLifetime); // теперь используется поле
            var payload = $"{userId}|{email}|{role}|{expireUtc:O}";
            var encrypted = CookieGenerator.Create(payload);

            var cookie = new HttpCookie("AuthCookie", encrypted)
            {
                HttpOnly = true,
                Secure = false,
                Expires = expireUtc
            };

            HttpContext.Current.Response.Cookies.Add(cookie);
        }

        public void SignOut()
        {
            
            var expired = new HttpCookie(CookieName)
            {
                Expires = DateTime.UtcNow.AddDays(-1),
                HttpOnly = true,
                Secure = HttpContext.Current.Request.IsSecureConnection
            };
            HttpContext.Current.Response.Cookies.Add(expired);
        }

        public List<UserModel> ListUsers()
        {
            var users = GetAllUsers();
            return users
                .Select(u => new UserModel
                {
                    Id = u.Id,
                    Name = u.Name,
                    Email = u.Email,
                    Roles = string.Join(", ", u.UserRoles.Select(ur => ur.Role.Name))
                })
                .ToList();
        }
    }
}
