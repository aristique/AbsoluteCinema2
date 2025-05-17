using System;
using System.Collections.Generic;

namespace ABSOLUTE_CINEMA.Models
{
    public class Actor
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; }
        public virtual ICollection<MovieActor> Movies { get; set; }
    }
}