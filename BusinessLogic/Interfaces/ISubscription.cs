// BusinessLogic/Interfaces/ISubscription.cs
using ABSOLUTE_CINEMA.Domain.DTO;

namespace ABSOLUTE_CINEMA.BusinessLogic.Interfaces
{
    /// <summary>
    /// Сервис для работы с подписками пользователя.
    /// </summary>
    public interface ISubscription
    {
        /// <summary>
        /// Проверяет, есть ли у текущего пользователя активная подписка.
        /// </summary>
        bool HasActive();

        /// <summary>
        /// Создаёт новую подписку, если у пользователя её нет.
        /// </summary>
        /// <param name="dto">Данные из формы оплаты.</param>
        void Subscribe(PaymentModel dto);
    }
}
