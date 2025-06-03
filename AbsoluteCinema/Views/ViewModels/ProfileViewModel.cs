using System.Collections.Generic;
using ABSOLUTE_CINEMA.Domain.DTO;

namespace ABSOLUTE_CINEMA.AbsoluteCinema.ViewModels
{
    public class ProfileViewModel
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public SubscriptionModel Subscription { get; set; }
        public IEnumerable<HistoryItemViewModel> History { get; set; }
    }
}
