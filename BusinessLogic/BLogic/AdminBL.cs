using System;
using ABSOLUTE_CINEMA.BusinessLogic.Interfaces;
using ABSOLUTE_CINEMA.BusinessLogic.Core;
using ABSOLUTE_CINEMA.Domain.Entities;

namespace ABSOLUTE_CINEMA.BusinessLogic.BLogic
{
    public class AdminBL : AdminApi, IAdmin
    {
        private readonly WebDbContext _db;

        public AdminBL(WebDbContext db)
        {
            _db = db;
        }

        public void BanUser(Guid userId)
        {
            base.BanUser(_db, userId);
        }

        public void ChangeUserRole(Guid userId, Guid roleId)
        {
            base.ChangeUserRole(_db, userId, roleId);
        }

        public void DeleteComments(Guid userId)
        {
            base.DeleteComments(_db, userId);
        }

        public void GrantSubscription(Guid userId, string subscriptionType)
        {
            base.GrantSubscription(_db, userId, subscriptionType);
        }
    }
}
