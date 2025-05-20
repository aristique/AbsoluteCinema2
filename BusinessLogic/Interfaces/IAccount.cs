using System;
using System.Collections.Generic;
using ABSOLUTE_CINEMA.Domain.DTO;
using ABSOLUTE_CINEMA.Domain.Entities;
using System.Collections.Generic;
using System;

namespace ABSOLUTE_CINEMA.BusinessLogic.Interfaces
{
    public interface IAccount
    {
        UserRegistrationResponse Register(Register dto);
        UserRoleType Login(Login dto);
        void SignIn(Guid userId, string email, UserRoleType role);
    void SignOut();                      
       List<UserViewModel> ListUsers();
    }
}
