﻿namespace PizzaTime.Orders
{
    public struct CompletdOrderInfo
    {
        public CompletdOrderInfo(int orderNumber, int productDeliveryWindowNumber)
        {
            OrderNumber = orderNumber;
            ProductDeliveryWindowNumber = productDeliveryWindowNumber;
        }

        public int OrderNumber { get; }
        public int ProductDeliveryWindowNumber { get; }
    }
}
