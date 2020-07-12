using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PizzaTime.Cashiers;
using PizzaTime.Orders;
using PizzaTime.ProductDeliveryWindows;
using PizzaTime.Products;
using PizzaTime.Restaurants;
using PizzaTime.Tables;

namespace PizzaTime.Clients
{
    public class Client : IClient
    {
        private static readonly Random _random = new Random();

        private readonly HashSet<int> _expectedOrdersNumbers;
        private IRestaurant _currentRestaurant;

        public Client()
        {
            _expectedOrdersNumbers = new HashSet<int>();
        }

        public IReadOnlyCollection<int> ExpectedOrdersNumbers => _expectedOrdersNumbers;

        public void EnterRestaurant(IRestaurant restaurant)
        {
            if (_currentRestaurant != null)
                throw new ClientIsAlreadyInRestaurantException(_currentRestaurant, "Client is already in the restaurant.");

            _currentRestaurant = restaurant;

            if (!_currentRestaurant.ProductDeliveryWindows.Any())
                return;

            foreach (var table in _currentRestaurant.Tables)
                table.OrderMarkedCompleted += OnOrderMarkedCompleted;
        }

        public bool LeaveRestaurant()
        {
            if (_currentRestaurant == null)
                return false;

            foreach (var table in _currentRestaurant.Tables)
                table.OrderMarkedCompleted -= OnOrderMarkedCompleted;

            _currentRestaurant = null;

            return true;
        }

        public void MakeOrder(ICollection<ProductType> productTypes)
        {
            if (productTypes == null)
                throw new ArgumentNullException(nameof(productTypes), "Argument is null.");

            if (!productTypes.Any())
                throw new ArgumentException($"{nameof(productTypes)} is empty.", nameof(productTypes));

            if (_currentRestaurant == null)
                throw new NullReferenceException($"{nameof(_currentRestaurant)} is null.");

            if (!_currentRestaurant.Cashiers.Any())
                throw new KeyNotFoundException($"{nameof(_currentRestaurant)} doesn't contains at least one cashier.");

            var сashier = _currentRestaurant.Cashiers[_random.Next(_currentRestaurant.Cashiers.Count)]; // считаем, что выбирает ближайшего.. или свободного

            сashier.AcceptOrder(productTypes, TakeOrderNumber);
        }

        private void OnOrderMarkedCompleted(object sender, OrderMarkedCompletedEventArgs e)
        {
            if (_expectedOrdersNumbers.Contains(e.CompletdOrderInfo.OrderNumber))
            {
                if (!_currentRestaurant.ProductDeliveryWindows.TryGetValue(e.CompletdOrderInfo.ProductDeliveryWindowNumber, out AbstractProductDeliveryWindow window))
                {
                    throw new KeyNotFoundException($"Restaurant does not have a window with number {e.CompletdOrderInfo.ProductDeliveryWindowNumber}.");
                }

                Console.WriteLine(TakeCompletedOrder(e.CompletdOrderInfo.OrderNumber, window));
            }
        }

        private AbstractCompletedOrder TakeCompletedOrder(int orderNumber, AbstractProductDeliveryWindow productDeliveryWindow)
        {
            _expectedOrdersNumbers.Remove(orderNumber);

            return productDeliveryWindow.ExtractCompletedOrderByNumber(orderNumber);
        }

        private void TakeOrderNumber(int orderNumber) => _expectedOrdersNumbers.Add(orderNumber);
    }
}
