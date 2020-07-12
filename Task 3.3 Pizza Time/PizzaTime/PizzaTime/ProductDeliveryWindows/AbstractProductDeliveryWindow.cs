using System;
using System.Collections.Generic;
using System.Text;
using PizzaTime.Orders;

namespace PizzaTime.ProductDeliveryWindows
{
    public abstract class AbstractProductDeliveryWindow
    {
        public AbstractProductDeliveryWindow(int windowNumber)
        {
            WindowNumber = windowNumber;
        }

        public abstract event EventHandler<CompletedOrderTakenEventArgs> CompletedOrderTaken;

        public int WindowNumber { get; }

        public abstract void AddCompletedOrder(AbstractCompletedOrder completedOrder);

        public abstract AbstractCompletedOrder ExtractCompletedOrderByNumber(int orderNumber);
    }
}
