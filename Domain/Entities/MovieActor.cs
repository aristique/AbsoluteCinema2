using System;

namespace ABSOLUTE_CINEMA.Domain.Entities
{
    public class MovieActor
    {
        public Guid MovieId { get; set; } 
        public Guid ActorId { get; set; } 
        public virtual Movie Movie { get; set; }
        public virtual Actor Actor { get; set; }
    }
}