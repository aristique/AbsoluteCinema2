using System;
using System.Collections.Generic;

namespace ABSOLUTE_CINEMA.Domain.Entities
{
    public class User
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public DateTime CreatedAt { get; set; }
        public virtual ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
        public virtual ICollection<Comment> Comments { get; set; }
            = new List<Comment>();
        public virtual ICollection<Subscription> Subscriptions { get; set; }
            = new List<Subscription>();
    }
}