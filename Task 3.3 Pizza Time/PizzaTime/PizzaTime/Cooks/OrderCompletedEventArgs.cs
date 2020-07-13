using System;
using PizzaTime.Orders;

namespace PizzaTime.Cooks
{
    public class OrderCompletedEventArgs : EventArgs
    {
        public OrderCompletedEventArgs(CompletdOrderInfo completdOrderInfo)
        {
            CompletdOrderInfo = completdOrderInfo;
        }

        public CompletdOrderInfo CompletdOrderInfo { get; }
    }
}
