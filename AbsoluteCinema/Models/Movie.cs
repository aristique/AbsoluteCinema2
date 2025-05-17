using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ABSOLUTE_CINEMA.Models
{
  
        public class Movie
        {
            public Guid Id { get; set; } = Guid.NewGuid();
        
            [Required]
            [StringLength(200)]
            public string Title { get; set; }
        
            [Required]
            [Range(1900, 2100)]
            public int Year { get; set; }
        
            [Required]
            [StringLength(100)]
            public string Country { get; set; }
        
            [Required]
            [DataType(DataType.MultilineText)]
            public string Description { get; set; }
        
            public string YouTubeVideoId { get; set; }
            
            
        
            public virtual ICollection<MovieGenre> Genres { get; set; } = new List<MovieGenre>();
            public virtual ICollection<MovieActor> Actors { get; set; } = new List<MovieActor>();
            public virtual ICollection<MovieDirector> Directors { get; set; } = new List<MovieDirector>();
            public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();
        }
    }
    
