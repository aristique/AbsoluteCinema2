using System;

namespace ABSOLUTE_CINEMA.Models
{
    public class BaseDbItem
    {
        public Guid Id { get; set; } = new Guid();
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}