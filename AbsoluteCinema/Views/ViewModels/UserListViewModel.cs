using System.Collections.Generic;

namespace ABSOLUTE_CINEMA.AbsoluteCinema.ViewModels
{
    public class UserListViewModel
    {
        public IEnumerable<UserViewModel> Users { get; set; }
        public IEnumerable<RoleViewModel> AllRoles { get; set; }

        public IEnumerable<string> SubscriptionTypes { get; set; }
    }
}