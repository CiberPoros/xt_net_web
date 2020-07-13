using System;
using PizzaTime.Orders;

namespace PizzaTime.OrdersControllers
{
    // Заказы должны как-то передаваться от кассира в поварам - будем считать, что за это отвечает контроллер
    public interface IOrdersController
    {
        event EventHandler<OrderAddedEventArgs> OrderAdded;

        void EnqueueOrder(AbstractOrder order);

        AbstractOrder DequeueOrder();
    }
}
