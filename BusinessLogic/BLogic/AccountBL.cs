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
        public UserRegistrationResponse Register(Register dto)
        {
            if (ExistsEmail(dto.Email))
                return new UserRegistrationResponse
                {
                    Success = false,
                    Message = "Email уже используется"
                };

            var user = new User
            {
                Id = Guid.NewGuid(),
                Name = dto.Name,
                Email = dto.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password),
                CreatedAt = DateTime.UtcNow
            };

            var createdUser = CreateUser(user);
            AssignRole(createdUser.Id, "User");

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

            var role = Enum.TryParse<UserRoleType>(roleName, out var rt) ? rt : UserRoleType.None;
            SignIn(user.Id, dto.Email, role);
            return role;
        }

        public void SignIn(Guid userId, string email, UserRoleType role)
        {
            var userData = $"{userId}|{role}";
            var encryptedUserData = ABSOLUTE_CINEMA.Helpers.CookieGenerator.Create(userData);

            var ticket = new FormsAuthenticationTicket(
                1,
                email,
                DateTime.Now,
                DateTime.Now.AddMinutes(30),
                false,
                encryptedUserData
            );

            var encryptedTicket = FormsAuthentication.Encrypt(ticket);
            var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket)
            {
                HttpOnly = true
            };

            HttpContext.Current.Response.Cookies.Add(cookie);
        }


        public void SignOut()
        {
            FormsAuthentication.SignOut();
        }

        public List<UserViewModel> ListUsers()
        {
            var users = GetAllUsers();

            return users.Select(u => new UserViewModel
            {
                Name = u.Name,
                Email = u.Email,
                Roles = string.Join(", ", u.UserRoles.Select(ur => ur.Role.Name))
            }).ToList();
        }
    }
}
