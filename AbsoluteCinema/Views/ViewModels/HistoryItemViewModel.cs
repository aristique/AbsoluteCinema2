
using System;

namespace ABSOLUTE_CINEMA.AbsoluteCinema.ViewModels
{

    public class HistoryItemViewModel
    {
   
        public Guid MovieId { get; set; }

  
        public string MovieTitle { get; set; }

        public string YouTubeVideoId { get; set; }

        public DateTime ViewedAt { get; set; }
    }
}
