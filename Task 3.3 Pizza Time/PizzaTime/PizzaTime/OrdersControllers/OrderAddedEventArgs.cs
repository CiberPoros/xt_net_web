using System;
using System.Collections.Generic;
using System.Text;
using PizzaTime.Orders;

namespace PizzaTime.OrdersControllers
{
    public class OrderAddedEventArgs : EventArgs
    {
        public OrderAddedEventArgs(IOrder order)
        {
            Order = order;
        }

        public IOrder Order { get; }
    }
}
