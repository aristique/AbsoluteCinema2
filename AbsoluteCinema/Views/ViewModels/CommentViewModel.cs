using System;

namespace ABSOLUTE_CINEMA.AbsoluteCinema.ViewModels
{
    public class CommentViewModel
    {
        public Guid Id { get; set; }

        public string Text { get; set; }

        public string UserName { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
