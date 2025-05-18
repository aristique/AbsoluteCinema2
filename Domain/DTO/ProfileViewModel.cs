using ABSOLUTE_CINEMA.Domain.Entities;

namespace ABSOLUTE_CINEMA.Domain.DTO
{
    public class ProfileViewModel
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public Subscription Subscription { get; set; }
    }
}