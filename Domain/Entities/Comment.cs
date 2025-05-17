using System;

namespace ABSOLUTE_CINEMA.Domain.Entities
{
    public class Comment
    {
        public Guid Id { get; set; }
        public string Text { get; set; }
        public DateTime CreatedAt { get; set; }
        public Guid MovieId { get; set; }
        public Guid UserId { get; set; }
    }
}