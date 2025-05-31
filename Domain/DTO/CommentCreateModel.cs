using System;
namespace ABSOLUTE_CINEMA.Domain.DTO
{
    public class CommentCreateModel
    {
        public Guid MovieId { get; set; }
        public string Text { get; set; }
    }
}