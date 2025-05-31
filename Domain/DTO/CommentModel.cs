using System;
using System.Collections.Generic;
namespace ABSOLUTE_CINEMA.Domain.DTO
{
    public class CommentModel
    {
        public Guid Id { get; set; }
        public string Text { get; set; }
        public string UserName { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}