using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using ABSOLUTE_CINEMA.Domain.Entities;
namespace ABSOLUTE_CINEMA.BusinessLogic.Core
{
    public class UserListApi
    {
        public List<User> GetAllUsers(WebDbContext db)
        {
            return db.Users.Include(u => u.UserRoles.Select(ur => ur.Role)).ToList();
        }
        public void UpdateRoles(WebDbContext db, Guid[] userIds, Guid[] roleIds)
        {
            if (userIds.Length != roleIds.Length)
                throw new ArgumentException("Массивы UserIds и RoleIds должны быть одинаковой длины.");

            for (int i = 0; i < userIds.Length; i++)
            {
                var uid = userIds[i];
                var rid = roleIds[i];
                var old = db.UserRoles.Where(ur => ur.UserId == uid).ToList();
                if (old.Any())
                    db.UserRoles.RemoveRange(old);
                db.UserRoles.Add(new UserRole { UserId = uid, RoleId = rid });
            }

            db.SaveChanges();
        }
    }
}
