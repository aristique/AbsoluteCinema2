using System;

namespace ABSOLUTE_CINEMA.Domain.DTO
{
    public class UserModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Roles { get; set; }
        public string Role { get; set; }

    }
}