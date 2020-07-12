using System;
using System.Collections.Generic;
using System.Text;
using PizzaTime.Orders;

namespace PizzaTime.Cashiers
{
    public class OrderAcceptedEventArgs : EventArgs
    {
        public OrderAcceptedEventArgs(AbstractOrder order)
        {
            Order = order ?? throw new ArgumentNullException(nameof(order), "Argument is null.");
        }

        public AbstractOrder Order { get; }
    }
}
