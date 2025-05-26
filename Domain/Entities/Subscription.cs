using System;

namespace ABSOLUTE_CINEMA.Domain.Entities
{
    public class Subscription
    {
        public Guid Id { get; set; }

        // ФК на пользователя
        public Guid UserId { get; set; }

        // Навигационное свойство — без него EF не сможет связать Subscription с User
        public virtual User User { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Type { get; set; }
        public bool IsActive { get; set; }
    }
}
