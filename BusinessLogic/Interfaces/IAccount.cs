using System;
using System.Collections.Generic;
using ABSOLUTE_CINEMA.Domain.DTO;
using ABSOLUTE_CINEMA.Domain.Entities;


namespace ABSOLUTE_CINEMA.BusinessLogic.Interfaces
{
    public interface IAccount
    {
        UserRegistrationResponse Register(Registerr dto);
        UserRoleType Login(Login dto);
        void SignIn(Guid userId, string email, UserRoleType role);
    void SignOut();                      
       List<UserModel> ListUsers();
        Guid GetUserIdByEmail(string email);
    }
}
