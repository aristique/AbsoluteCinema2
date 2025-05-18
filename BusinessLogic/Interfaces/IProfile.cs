using ABSOLUTE_CINEMA.Domain.DTO;
namespace ABSOLUTE_CINEMA.BusinessLogic.Interfaces
{
    public interface IProfile
    {
        ProfileViewModel GetProfile(string email);
    }
}
