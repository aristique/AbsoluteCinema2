using System;

namespace ABSOLUTE_CINEMA.Domain.Entities
{
    public class MovieDirector
    {
        // Внешние ключи
        public Guid MovieId { get; set; }
        public Guid DirectorId { get; set; }

        // Навигационные свойства
        public virtual Movie Movie { get; set; }
        public virtual Director Director { get; set; }
    }
}