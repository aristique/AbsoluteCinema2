using System;

namespace ABSOLUTE_CINEMA.Models
{
 
        public class MovieGenre
        {
            public Guid MovieId { get; set; }
            public Guid GenreId { get; set; }
            public virtual Movie Movie { get; set; }
            public virtual Genre Genre { get; set; }
        }
    }
