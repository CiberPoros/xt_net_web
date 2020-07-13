using System;
using PizzaTime.Orders;

namespace PizzaTime.Tables
{
    public class OrderMarkedCompletedEventArgs : EventArgs
    {
        public OrderMarkedCompletedEventArgs(CompletdOrderInfo completdOrderInfo)
        {
            CompletdOrderInfo = completdOrderInfo;
        }

        public CompletdOrderInfo CompletdOrderInfo { get; }
    }
}
