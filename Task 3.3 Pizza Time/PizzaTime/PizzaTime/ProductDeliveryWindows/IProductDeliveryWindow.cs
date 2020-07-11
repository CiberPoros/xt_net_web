using System;
using System.Collections.Generic;
using System.Text;
using PizzaTime.Orders;

namespace PizzaTime.ProductDeliveryWindows
{
    public interface IProductDeliveryWindow
    {
        event EventHandler<CompletedOrderTakenEventArgs> CompletedOrderTaken;

        void AddCompletedOrder(ICompletedOrder completedOrder);

        ICompletedOrder ExtractCompletedOrderByNumber(int orderNumber);
    }
}
