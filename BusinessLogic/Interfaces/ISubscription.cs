
using ABSOLUTE_CINEMA.Domain.DTO;

namespace ABSOLUTE_CINEMA.BusinessLogic.Interfaces
{

    public interface ISubscription
    {

        bool HasActive();

        void Subscribe(PaymentModel dto);
    }
}
