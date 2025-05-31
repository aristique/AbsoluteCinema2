using System;
using System.Collections.Generic;
using ABSOLUTE_CINEMA.Domain.DTO;
using ABSOLUTE_CINEMA.Domain.Entities;

namespace ABSOLUTE_CINEMA.BusinessLogic.Interfaces
{
    public interface IUserList
    {
        List<UserModel> GetAllUsers();
        List<Role> GetAllRoles();
        void UpdateRoles(Guid[] userIds, Guid[] roleIds);
    }
}
