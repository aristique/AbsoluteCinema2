using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ABSOLUTE_CINEMA.Models
{
    public class Subscription
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        
        [ForeignKey("User")]
        public Guid UserId { get; set; }
        public virtual User User { get; set; }
        
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Type { get; set; }
        public bool IsActive { get; set; } = true;
    }
}