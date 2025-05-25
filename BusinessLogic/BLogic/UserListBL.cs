using System;
using System.Collections.Generic;
using ABSOLUTE_CINEMA.BusinessLogic.Interfaces;
using ABSOLUTE_CINEMA.BusinessLogic.Core;
using ABSOLUTE_CINEMA.Domain.Entities;

namespace ABSOLUTE_CINEMA.BusinessLogic.BLogic
{
    public class UserListBL : UserListApi, IUserList
    {
        public List<Role> GetAllRoles()
        {
            return GetAllRoless();
        }

        public List<User> GetAllUsers()
        {
            return GetAllUserss();
        }

        public void UpdateRoles(Guid[] userIds, Guid[] roleIds)
        {
            UpdateRoless(userIds, roleIds);
        }
    }
}
