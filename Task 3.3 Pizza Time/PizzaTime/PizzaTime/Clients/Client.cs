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
    public class Client : IClient
    {
        private readonly HashSet<int> _expectedOrdersNumbers;

        public Client()
        {
            _expectedOrdersNumbers = new HashSet<int>();
        }

        public IReadOnlyCollection<int> ExpectedOrdersNumbers => _expectedOrdersNumbers;

        public void MakeOrder(ICollection<ProductType> productTypes, ICashier cashier) =>
            cashier.AcceptOrder(productTypes, TakeOrderNumber);

        public void OnOrderMarkedCompleted(object sender, OrderMarkedCompletedEventArgs e)
        {
            if (_expectedOrdersNumbers.Contains(e.OrderNumber))
                Console.WriteLine(TakeCompletedOrder(e.OrderNumber, ProductDeliveryWindow.Instance));
        }

        public ICompletedOrder TakeCompletedOrder(int orderNumber, IProductDeliveryWindow productDeliveryWindow)
        {
            _expectedOrdersNumbers.Remove(orderNumber);

            return productDeliveryWindow.ExtractCompletedOrderByNumber(orderNumber);
        }

        public void TakeOrderNumber(int orderNumber) => _expectedOrdersNumbers.Add(orderNumber);
    }
}
