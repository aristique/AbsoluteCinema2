using ABSOLUTE_CINEMA.Domain.DTO;
namespace ABSOLUTE_CINEMA.BusinessLogic.Interfaces
{
    public interface IProfile
    {
        ProfileModel GetProfile(string email);
    }
}
