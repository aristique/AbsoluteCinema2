using System;

namespace ABSOLUTE_CINEMA.Domain.Entities
{
    public class BaseDbItem
    {
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}