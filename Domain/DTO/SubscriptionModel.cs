using System;
namespace ABSOLUTE_CINEMA.Domain.DTO
{
    public class SubscriptionModel
    {
        public Guid Id { get; set; }
        public string Type { get; set; }        
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsActive { get; set; }
       
    }
}