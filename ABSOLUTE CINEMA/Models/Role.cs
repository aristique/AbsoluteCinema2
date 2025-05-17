using System;
using System.Collections;
using System.Collections.Generic;

namespace ABSOLUTE_CINEMA.Models
{
    public class Role
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; }
        public ICollection<UserRole> UserRoles { get; set; } 
    }
}