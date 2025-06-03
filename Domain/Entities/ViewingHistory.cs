using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ABSOLUTE_CINEMA.Domain.Entities
{

    [Table("ViewingHistory", Schema = "dbo")]
    public class ViewingHistory
    {
        public Guid Id { get; set; }


        public Guid UserId { get; set; }

        public Guid MovieId { get; set; }
        public DateTime ViewedAt { get; set; }

        [ForeignKey("MovieId")]
        public virtual Movie Movie { get; set; }
    }
}
