using System;
using System.Collections.Generic;
using System.Text;
using PizzaTime.Orders;
using PizzaTime.OrdersControllers;
using PizzaTime.ProductDeliveryWindows;

namespace PizzaTime.Cooks
{
    public interface ICook
    {
        event EventHandler<OrderCompletedEventArgs> OrderCompleted;

        AbstractProductDeliveryWindow NearProductDeliveryWindow { set; }
        IOrderController OrderController { set; }
    }
}
