using System;
using System.Collections.Generic;
using System.Linq;
using ABSOLUTE_CINEMA.Domain.Entities;
using ABSOLUTE_CINEMA.Domain.DTO;


namespace ABSOLUTE_CINEMA.BusinessLogic.Core
{
    public class AccountApi
    {
        public bool ExistsEmail(WebDbContext db, string email)
            => db.Users.Any(u => u.Email == email);

        public User CreateUser(WebDbContext db, Register dto)
        {
            var user = new User
            {
                Id = Guid.NewGuid(),
                Name = dto.Name,
                Email = dto.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password),
                CreatedAt = DateTime.UtcNow
            };
            db.Users.Add(user);
            db.SaveChanges();
            return user;
        }

        public void AssignRole(WebDbContext db, Guid userId, string roleName)
        {
            var role = db.Roles.First(r => r.Name == roleName);
            db.UserRoles.Add(new UserRole { UserId = userId, RoleId = role.Id });
            db.SaveChanges();
        }

        public User FindByEmail(WebDbContext db, string email)
            => db.Users.FirstOrDefault(u => u.Email == email);

        public List<string> GetRoles(WebDbContext db, Guid userId)
            => db.UserRoles.Where(ur => ur.UserId == userId)
                           .Select(ur => ur.Role.Name).ToList();

        public List<UserViewModel> ListUsers(WebDbContext db)
        {
            return db.Users.Select(u => new UserViewModel
            {
                Name = u.Name,
                Email = u.Email,
                Roles = string.Join(",", u.UserRoles.Select(ur => ur.Role.Name))
            }).ToList();
        }
    }
}
