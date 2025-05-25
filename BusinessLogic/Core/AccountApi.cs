using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using ABSOLUTE_CINEMA.BusinessLogic.Interfaces;
using ABSOLUTE_CINEMA.BusinessLogic.Core;
using ABSOLUTE_CINEMA.Domain.DTO;
using ABSOLUTE_CINEMA.Domain.Entities;


namespace ABSOLUTE_CINEMA.BusinessLogic.Core
{
    public class AccountApi
    {
        public bool ExistsEmail(string email)
        {
            using (var db = new WebDbContext())
            {
                return db.Users.Any(u => u.Email == email);
            }
        }

        public User CreateUser(User user)
        {
            using (var db = new WebDbContext())
            {
                db.Users.Add(user);
                db.SaveChanges();
                return user;
            }
        }

        public void AssignRole(Guid userId, string roleName)
        {
            using (var db = new WebDbContext())
            {
                var role = db.Roles.FirstOrDefault(r => r.Name == roleName);
                if (role != null)
                {
                    db.UserRoles.Add(new UserRole { UserId = userId, RoleId = role.Id });
                    db.SaveChanges();
                }
            }
        }

        public User FindByEmail(string email)
        {
            using (var db = new WebDbContext())
            {
                return db.Users.FirstOrDefault(u => u.Email == email);
            }
        }

        public List<string> GetRoles(Guid userId)
        {
            using (var db = new WebDbContext())
            {
                return db.UserRoles
                         .Where(ur => ur.UserId == userId)
                         .Select(ur => ur.Role.Name)
                         .ToList();
            }
        }

        public List<User> GetAllUsers()
        {
            using (var db = new WebDbContext())
            {
                return db.Users
                         .Include("UserRoles.Role") 
                         .ToList();
            }
        }
    }
}
