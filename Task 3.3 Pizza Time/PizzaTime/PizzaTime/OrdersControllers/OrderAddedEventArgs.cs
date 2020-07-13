using System;
using PizzaTime.Orders;

namespace PizzaTime.OrdersControllers
{
    public class OrderAddedEventArgs : EventArgs
    {
        public OrderAddedEventArgs(AbstractOrder order)
        {
            Order = order;
        }

        public AbstractOrder Order { get; }
    }
}
