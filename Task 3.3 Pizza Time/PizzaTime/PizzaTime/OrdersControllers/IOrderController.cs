using System;
using System.Collections.Generic;
using System.Text;
using PizzaTime.Orders;

namespace PizzaTime.OrdersControllers
{
    public interface IOrderController
    {
        event EventHandler<OrderAddedEventArgs> OrderAdded;

        void EnqueueOrder(IOrder order);

        IOrder DequeueOrder();
    }
}
