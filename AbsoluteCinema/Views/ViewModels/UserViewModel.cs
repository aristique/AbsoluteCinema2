
using System;

namespace ABSOLUTE_CINEMA.AbsoluteCinema.ViewModels
{
    public class UserViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public bool IsBanned { get; set; }
    }
}
