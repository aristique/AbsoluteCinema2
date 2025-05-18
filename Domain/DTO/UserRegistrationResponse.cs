using System;

namespace ABSOLUTE_CINEMA.Domain.DTO
{
    public class UserRegistrationResponse
    {
        public bool Success { get; set; }
        public Guid UserId { get; set; }
        public string Message { get; set; }
    }
}
