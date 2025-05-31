using System;
using System.Collections.Generic;
using System.Linq;
using ABSOLUTE_CINEMA.BusinessLogic.Interfaces;
using ABSOLUTE_CINEMA.BusinessLogic.Core;
using ABSOLUTE_CINEMA.Domain.Entities;
using ABSOLUTE_CINEMA.Domain.DTO;

namespace ABSOLUTE_CINEMA.BusinessLogic.BLogic
{
    public class UserListBL : UserListApi, IUserList
    {
        public List<Role> GetAllRoles()
        {
            return GetAllRoless();
        }

        public List<UserModel> GetAllUsers()
        {
            return GetAllUserss()
                .Select(u => new UserModel
                {
                    Id = u.Id,
                    Name = u.Name,
                    Email = u.Email,
                    Roles = string.Join(", ", u.UserRoles.Select(ur => ur.Role.Name))
                })
                .ToList();
        }

        public void UpdateRoles(Guid[] userIds, Guid[] roleIds)
        {
            UpdateRoless(userIds, roleIds);
        }
    }
}
