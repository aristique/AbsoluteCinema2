using System;

namespace ABSOLUTE_CINEMA.Domain.Entities
{
    public class MovieGenre
    {
        public Guid MovieId { get; set; }
        public Guid GenreId { get; set; }
    }
}