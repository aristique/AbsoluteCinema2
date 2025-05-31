

using System.Collections.Generic;
using System;
namespace ABSOLUTE_CINEMA.Domain.DTO
{
    public class CreateMovieModel
    {
        public string Title { get; set; }
        public int Year { get; set; }
        public string Country { get; set; }
        public string Description { get; set; }
        public string YouTubeVideoId { get; set; }

        public List<Guid> SelectedGenres { get; set; }
        public List<Guid> SelectedActors { get; set; }
        public List<Guid> SelectedDirectors { get; set; }
    }
}