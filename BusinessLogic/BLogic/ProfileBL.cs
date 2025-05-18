using ABSOLUTE_CINEMA.BusinessLogic.Core;
using ABSOLUTE_CINEMA.BusinessLogic.Interfaces;
using ABSOLUTE_CINEMA.Domain.DTO;
namespace ABSOLUTE_CINEMA.BusinessLogic.BLogic
{
    public class ProfileBL : ProfileApi, IProfile
    {
        private readonly WebDbContext _db;

        public ProfileBL(WebDbContext db)
        {
            _db = db;
        }

        public ProfileViewModel GetProfile(string email)
        {
            return Load(_db, email);
        }
    }
}