using System;
using System.Collections.Generic;
using System.Text;
using PizzaTime.Cashiers;
using PizzaTime.Clients;
using PizzaTime.Cooks;
using PizzaTime.Orders;
using PizzaTime.ProductDeliveryWindows;

namespace PizzaTime.Tables
{
    // Табло, на котором высвечиваются готовящиеся заказы и приготовленные заказы, которые еще не забрали
    public interface ITable
    {
        event EventHandler<OrderMarkedCompletedEventArgs> OrderMarkedCompleted;

        void OnOrderAccepted(object sender, OrderAcceptedEventArgs e);
        void OnCompletedOrderTaken(object sender, CompletedOrderTakenEventArgs e);
        void OnOrderCompleted(object sender, OrderCompletedEventArgs e);
    }
}
