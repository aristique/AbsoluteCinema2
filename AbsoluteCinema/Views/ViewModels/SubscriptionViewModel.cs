using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ABSOLUTE_CINEMA.AbsoluteCinema.ViewModels
{
    public class SubscriptionViewModel
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsActive => EndDate > DateTime.UtcNow;
    }

}