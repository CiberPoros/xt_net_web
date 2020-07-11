using System;
using System.Collections.Generic;
using System.Text;
using PizzaTime.Cashiers;
using PizzaTime.Orders;
using PizzaTime.ProductDeliveryWindows;
using PizzaTime.Products;
using PizzaTime.Tables;

namespace PizzaTime.Clients
{
    public interface IClient
    {
        IReadOnlyCollection<int> ExpectedOrdersNumbers { get; }

        // returns order number
        void MakeOrder(ICollection<ProductType> productTypes, ICashier cashier);

        void OnOrderMarkedCompleted(object sender, OrderMarkedCompletedEventArgs e);

        ICompletedOrder TakeCompletedOrder(int orderNumber, IProductDeliveryWindow productDeliveryWindow);

        void TakeOrderNumber(int orderNumber);
    }
}
