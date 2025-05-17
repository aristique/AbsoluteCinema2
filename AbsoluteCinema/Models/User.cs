using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ABSOLUTE_CINEMA.Models
{
    public class User
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        
        [Required]
        public string PasswordHash { get; set; }
        
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();

        public virtual ICollection<Subscription> Subscriptions { get; set; } = new List<Subscription>();
    }
}