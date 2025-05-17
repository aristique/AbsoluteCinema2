using System;
using System.Collections.Generic;

namespace ABSOLUTE_CINEMA.Models
{
    public class Genre
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; }
        public virtual ICollection<MovieGenre> Movies { get; set; }
    }
}