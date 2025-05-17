using System;

namespace ABSOLUTE_CINEMA.Models
{
    public class MovieDirector
    {
        public Guid MovieId { get; set; }
        public Guid DirectorId { get; set; }
        public virtual Movie Movie { get; set; }
        public virtual Director Director { get; set; }
    }
}