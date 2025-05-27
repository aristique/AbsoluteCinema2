using System;
using ABSOLUTE_CINEMA.Domain.Entities;
namespace ABSOLUTE_CINEMA.AbsoluteCinema.ViewModels
    {
        public class ProfileViewModel
        {
            public string Name { get; set; }
            public string Email { get; set; }
            public Subscription Subscription { get; set; }
        }
    }