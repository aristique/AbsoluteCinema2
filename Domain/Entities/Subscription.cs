using System;

namespace ABSOLUTE_CINEMA.Domain.Entities
{
    public class Subscription
    {
        public Guid Id { get; set; }

        
        public Guid UserId { get; set; }

  
        public virtual User User { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Type { get; set; }
        public bool IsActive { get; set; }
    }
}
