using System;
using ABSOLUTE_CINEMA.BusinessLogic.Interfaces;
using ABSOLUTE_CINEMA.BusinessLogic.Core;
using ABSOLUTE_CINEMA.Domain.Entities;
namespace ABSOLUTE_CINEMA.BusinessLogic.BLogic
{
    public class AdminBL : AdminApi, IAdmin
    {
        public void BanUser(Guid userId)
        {
            BanUserr(userId);
        }

        public void ChangeUserRole(Guid userId, Guid roleId)
        {
            ChangeUserRolee(userId, roleId);
        }

        public void DeleteComments(Guid userId)
        {
           DeleteCommentss(userId);
        }

        public void GrantSubscription(Guid userId, string subscriptionType)
        {
            GrantSubscriptionn(userId, subscriptionType);
        }
    }
}
