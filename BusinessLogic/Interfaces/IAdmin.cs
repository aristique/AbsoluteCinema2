using System;
using System.Collections.Generic;
using ABSOLUTE_CINEMA.Domain.Entities;

namespace ABSOLUTE_CINEMA.BusinessLogic.Interfaces
{
    public interface IAdmin
    {
        void BanUser(Guid userId);
        void ChangeUserRole(Guid userId, Guid roleId);
        void DeleteComments(Guid commentId);
        void GrantSubscription(Guid userId, string subscriptionType);
    }
}

