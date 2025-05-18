using ABSOLUTE_CINEMA.Domain.DTO;
using ABSOLUTE_CINEMA.Domain.Entities;
using System.Collections.Generic;
using System;

public interface IAccount
{
    UserRegistrationResponse Register(Register dto);
    UserRoleType Login(Login dto);
    void SignIn(Guid userId, string email, UserRoleType role);
    void SignOut();                      
    List<UserViewModel> ListUsers();
}
