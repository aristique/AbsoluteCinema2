using System;
using System.Collections.Generic;

namespace ABSOLUTE_CINEMA.AbsoluteCinema.ViewModels
{
    public class MovieDetailsViewModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public int Year { get; set; }
        public string Country { get; set; }
        public string Description { get; set; }
        public string YouTubeVideoId { get; set; }

        public List<string> Genres { get; set; }
        public List<string> Actors { get; set; }
        public List<string> Directors { get; set; }
        public List<CommentViewModel> Comments { get; set; } = new List<CommentViewModel>();
    }
}
