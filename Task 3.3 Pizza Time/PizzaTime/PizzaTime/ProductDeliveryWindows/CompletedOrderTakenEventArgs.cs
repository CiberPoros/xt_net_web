using System;
using PizzaTime.Orders;

namespace PizzaTime.ProductDeliveryWindows
{
    public class CompletedOrderTakenEventArgs : EventArgs
    {
        public CompletedOrderTakenEventArgs(AbstractCompletedOrder completedOrder)
        {
            CompletedOrder = completedOrder ?? throw new ArgumentNullException(nameof(completedOrder), "Argument is null.");
        }

        public AbstractCompletedOrder CompletedOrder { get; }
    }
}
