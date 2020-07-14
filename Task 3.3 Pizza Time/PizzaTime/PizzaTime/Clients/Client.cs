using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PizzaTime.ProductDeliveryWindows;
using PizzaTime.Products;
using PizzaTime.Restaurants;
using PizzaTime.Tables;

namespace PizzaTime.Clients
{
    public class Client : IClient
    {
        private const int MIN_MOVING_TIME_TO_PRODUCT_WINDOW_IN_MILLISECONDS = 500;
        private const int MAX_MOVING_TIME_TO_PRODUCT_WINDOW_IN_MILLISECONDS = 2000;

        private static readonly Random _random = new Random();

        private readonly HashSet<int> _expectedOrdersNumbers;
        private IRestaurantUI _currentRestaurant;

        public Client()
        {
            _expectedOrdersNumbers = new HashSet<int>();
        }

        public IReadOnlyCollection<int> ExpectedOrdersNumbers => _expectedOrdersNumbers;

        public void EnterRestaurant(IRestaurantUI restaurant)
        {
            if (_currentRestaurant != null)
                throw new ClientIsAlreadyInRestaurantException(_currentRestaurant, "Client is already in the restaurant.");

            _currentRestaurant = restaurant;
        }

        public bool LeaveRestaurant()
        {
            if (_currentRestaurant == null)
                return false;

            // Вообще, это не обязательно, если клиент заберет все сделанные им заказы, он и так отпишется от всех табло
            // Но ведь он может уйти, не забрав заказ... Только как остальные сущности будут вести себя в таком случае? // TODO: подумать
            foreach (var table in _currentRestaurant.Tables)
                table.OrderMarkedCompleted -= OnOrderMarkedCompleted;

            _currentRestaurant = null;

            return true;
        }

        public virtual void MakeOrder(ICollection<ProductType> productTypes)
        {
            if (productTypes == null)
                throw new ArgumentNullException(nameof(productTypes), "Argument is null.");

            if (!productTypes.Any())
                throw new ArgumentException($"{nameof(productTypes)} is empty.", nameof(productTypes));

            if (_currentRestaurant == null)
                throw new NullReferenceException($"{nameof(_currentRestaurant)} is null.");

            if (!_currentRestaurant.Cashiers.Any())
                throw new KeyNotFoundException($"{nameof(_currentRestaurant)} doesn't contains at least one cashier.");

            foreach (var table in _currentRestaurant.Tables)
                table.OrderMarkedCompleted += OnOrderMarkedCompleted;

            // считаем, что выбирает ближайшего.. или свободного
            var сashier = _currentRestaurant.Cashiers.ElementAt(_random.Next(_currentRestaurant.Cashiers.Count)); 

            сashier.AcceptOrder(productTypes, TakeOrderNumber);
        }

        public async virtual void OnOrderMarkedCompleted(object sender, OrderMarkedCompletedEventArgs e)
        {
            // имитация похода до окна выдачи продуктов
            await Task.Delay(TimeSpan.FromMilliseconds(_random.Next(MIN_MOVING_TIME_TO_PRODUCT_WINDOW_IN_MILLISECONDS, MAX_MOVING_TIME_TO_PRODUCT_WINDOW_IN_MILLISECONDS)));

            IProductDeliveryWindow productDeliveryWindow;
            try
            {
                productDeliveryWindow = _currentRestaurant.GetProductDeliveryWindowByNumber(e.CompletdOrderInfo.ProductDeliveryWindowNumber);
                TakeCompletedOrder(e.CompletdOrderInfo.OrderNumber, productDeliveryWindow);
            }
            catch (KeyNotFoundException)
            {
                // Этот момент не критичен, т.к. клиент может быть подписан сразу на несколько табло, и этот метод тригерят сразу много эвентов.
                // А номер заказа удаляется сразу после первого триггера эвента
                // Поэтому на остальные можно не обращать внимания
                // Этот момент я бы просто логировал
            }
            catch
            {
                throw;
            }
        }

        public virtual void TakeCompletedOrder(int orderNumber, IProductDeliveryWindow productDeliveryWindow)
        {
            if (!_expectedOrdersNumbers.Remove(orderNumber))
                throw new KeyNotFoundException($"Client did not place an order with this number.");

            var completedOrder = productDeliveryWindow.ExtractCompletedOrderByNumber(orderNumber);
            if (completedOrder == null)
                throw new ArgumentException($"Order with this number doesn't conteins in this product delivery window.", nameof(orderNumber));

            if (!_expectedOrdersNumbers.Any())
            {
                foreach (var table in _currentRestaurant.Tables)
                {
                    table.OrderMarkedCompleted -= OnOrderMarkedCompleted;
                }
            }

            Console.WriteLine(completedOrder);
        }

        private void TakeOrderNumber(int orderNumber) => _expectedOrdersNumbers.Add(orderNumber);
    }
}
