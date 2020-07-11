using System;
using System.Collections.Generic;
using System.Text;
using PizzaTime.Orders;

namespace PizzaTime.Cooks
{
    public class OrderCompletedEventArgs : EventArgs
    {
        public OrderCompletedEventArgs(ICompletedOrder completedOrder)
        {
            CompletedOrder = completedOrder ?? throw new ArgumentNullException(nameof(completedOrder), "Argument is null.");
        }

        public ICompletedOrder CompletedOrder { get; }
    }
}
