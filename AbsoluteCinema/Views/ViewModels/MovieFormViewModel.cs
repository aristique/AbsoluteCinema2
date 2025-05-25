using System;
using System.Collections.Generic;
namespace ABSOLUTE_CINEMA.AbsoluteCinema.ViewModels
{
    public class MovieFormViewModel
    {
        public string Title { get; set; }
        public int Year { get; set; }
        public string Country { get; set; }
        public string Description { get; set; }
        public string YouTubeVideoId { get; set; }
        public List<Guid> SelectedGenres { get; set; } = new List<Guid>();
        public List<Guid> SelectedActors { get; set; } = new List<Guid>();
        public List<Guid> SelectedDirectors { get; set; } = new List<Guid>();

    }
}
