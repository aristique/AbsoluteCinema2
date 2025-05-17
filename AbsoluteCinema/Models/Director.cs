using System;
using System.Collections.Generic;

namespace ABSOLUTE_CINEMA.Models
{
    public class Director
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; }
        public virtual ICollection<MovieDirector> Movies { get; set; }
    }
}