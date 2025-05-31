namespace ABSOLUTE_CINEMA.Domain.DTO
{
    public class ProfileModel
    {
        public string Name { get; set; }
        public string Email { get; set; }


        public SubscriptionModel Subscription { get; set; }
    }
}
