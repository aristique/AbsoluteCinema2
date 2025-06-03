using ABSOLUTE_CINEMA.AbsoluteCinema.ViewModels;
using System.Collections.Generic;
using System;



namespace ABSOLUTE_CINEMA.AbsoluteCinema.ViewModels
{
    public class MovieViewModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public int Year { get; set; }
        public string YouTubeVideoId { get; set; }
        public List<GenreViewModel> Genres { get; set; }
    }
}