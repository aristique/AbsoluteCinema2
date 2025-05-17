using System;

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
    }
}