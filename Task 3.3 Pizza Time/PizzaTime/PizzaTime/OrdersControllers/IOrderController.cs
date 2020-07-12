using System;
using System.Collections.Generic;
using System.Text;
using PizzaTime.Orders;

namespace PizzaTime.OrdersControllers
{
    // Заказы должны как-то передаваться от кассира в поварам - будем считать, что за это отвечает контроллер
    public interface IOrderController
    {
        event EventHandler<OrderAddedEventArgs> OrderAdded;

        void EnqueueOrder(AbstractOrder order);

        AbstractOrder DequeueOrder();
    }
}
