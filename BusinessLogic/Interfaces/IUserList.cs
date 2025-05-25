using System;
using System.Collections.Generic;
using ABSOLUTE_CINEMA.Domain.Entities;

namespace ABSOLUTE_CINEMA.BusinessLogic.Interfaces
{
    public interface IUserList
    {
        List<User> GetAllUsers();
        void UpdateRoles(Guid[] userIds, Guid[] roleIds);
        List<Role> GetAllRoles();
    }
}