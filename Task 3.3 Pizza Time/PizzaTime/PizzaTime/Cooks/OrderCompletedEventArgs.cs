using System;
using System.Collections.Generic;
using System.Text;
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
