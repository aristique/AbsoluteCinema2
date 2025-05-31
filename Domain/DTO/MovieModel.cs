using System;
using System.Collections.Generic;

namespace ABSOLUTE_CINEMA.Domain.DTO
{
    public class MovieModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public int Year { get; set; }
        public string Country { get; set; }
        public string Description { get; set; }
        public string YouTubeVideoId { get; set; }

        public List<GenreModel> Genres { get; set; }
        public List<ActorModel> Actors { get; set; }
        public List<DirectorModel> Directors { get; set; }
        public List<CommentModel> Comments { get; set; }
        
    }
}