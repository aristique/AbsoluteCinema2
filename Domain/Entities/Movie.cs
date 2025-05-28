using System;
using System.Collections.Generic;

namespace ABSOLUTE_CINEMA.Domain.Entities
{
    public class Movie
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public int Year { get; set; }
        public string Country { get; set; }
        public string Description { get; set; }
        public string YouTubeVideoId { get; set; }
        public int DetailsViewCount { get; set; } = 0;
        public virtual ICollection<Comment> Comments { get; set; }
    = new List<Comment>();
        public virtual ICollection<MovieGenre> Genres { get; set; } = new List<MovieGenre>();

        
        public virtual ICollection<MovieActor> Actors { get; set; } = new List<MovieActor>();
        public virtual ICollection<MovieDirector> Directors { get; set; } = new List<MovieDirector>();

    }
}