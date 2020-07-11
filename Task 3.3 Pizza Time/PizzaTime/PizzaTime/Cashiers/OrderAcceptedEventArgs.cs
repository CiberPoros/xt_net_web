using System;
using System.Collections.Generic;
using System.Text;
using PizzaTime.Orders;

namespace PizzaTime.Cashiers
{
    public class OrderAcceptedEventArgs : EventArgs
    {
        public OrderAcceptedEventArgs(IOrder order)
        {
            Order = order ?? throw new ArgumentNullException(nameof(order), "Argument is null.");
        }

        public IOrder Order { get; }
    }
}
