
using System;

namespace ABSOLUTE_CINEMA.Domain.DTO
{
    public class ViewingHistoryDto
    {
        public Guid Id { get; set; }
        public Guid MovieId { get; set; }
        public DateTime ViewedAt { get; set; }
        public string MovieTitle { get; set; }
        public string YouTubeVideoId { get; set; }
    }
}
