using ABSOLUTE_CINEMA.BusinessLogic.Core;
using ABSOLUTE_CINEMA.BusinessLogic.Interfaces;
using ABSOLUTE_CINEMA.Domain.DTO;

namespace ABSOLUTE_CINEMA.BusinessLogic.BLogic
{
    public class ProfileBL : ProfileApi, IProfile
    {
        public ProfileViewModel GetProfile(string email)
        {
            return Loadd(email);
        }
    }
}
