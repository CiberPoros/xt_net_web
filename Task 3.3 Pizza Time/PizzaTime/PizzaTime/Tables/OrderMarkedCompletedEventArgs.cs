using System;
using System.Collections.Generic;
using System.Text;
using PizzaTime.Orders;

namespace PizzaTime.Tables
{
    public class OrderMarkedCompletedEventArgs : EventArgs
    {
        public OrderMarkedCompletedEventArgs(int orderNumber)
        {
            OrderNumber = orderNumber;
        }

        public int OrderNumber { get; }
    }
}
