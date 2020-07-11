using System;
using System.Collections.Generic;
using System.Text;
using PizzaTime.Orders;

namespace PizzaTime.ProductDeliveryWindows
{
    public class CompletedOrderTakenEventArgs : EventArgs
    {
        public CompletedOrderTakenEventArgs(ICompletedOrder completedOrder)
        {
            CompletedOrder = completedOrder ?? throw new ArgumentNullException(nameof(completedOrder), "Argument is null.");
        }

        public ICompletedOrder CompletedOrder { get; }
    }
}
