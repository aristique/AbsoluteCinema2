using System;
using System.Collections.Generic;
using ABSOLUTE_CINEMA.BusinessLogic.Interfaces;
using ABSOLUTE_CINEMA.BusinessLogic.Core;
using ABSOLUTE_CINEMA.Domain.Entities;

namespace ABSOLUTE_CINEMA.BusinessLogic.BLogic
{
    public class UserListBL : UserListApi, IUserList
    {
        private readonly WebDbContext _db;

        public UserListBL(WebDbContext db)
        {
            _db = db;
        }

        public List<User> GetAllUsers()
        {
            return base.GetAllUsers(_db);
        }

        public void UpdateRoles(Guid[] userIds, Guid[] roleIds)
        {
            base.UpdateRoles(_db, userIds, roleIds);
        }
    }
}
