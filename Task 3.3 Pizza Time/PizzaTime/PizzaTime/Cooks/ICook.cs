using System;
using System.Collections.Generic;
using System.Text;
using PizzaTime.Orders;
using PizzaTime.OrdersControllers;

namespace PizzaTime.Cooks
{
    public interface ICook
    {
        event EventHandler<OrderCompletedEventArgs> OrderCompleted;

        void OnOrderAdded(object sender, OrderAddedEventArgs e);
    }
}
