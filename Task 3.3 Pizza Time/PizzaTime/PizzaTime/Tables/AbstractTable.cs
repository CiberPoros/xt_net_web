using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PizzaTime.Cashiers;
using PizzaTime.Cooks;
using PizzaTime.ProductDeliveryWindows;
using PizzaTime.Restaurants;

namespace PizzaTime.Tables
{
    // Табло, на котором высвечиваются готовящиеся заказы и приготовленные заказы, которые еще не забрали
    public abstract class AbstractTable : RestaurantObject
    {
        private const int MIN_DELAY_TIME_IN_MILLISECONDS = 500;
        private const int MAX_DELAY_TIME_IN_MILLISECONDS = 1500;

        private static readonly Random _random = new Random();

        protected readonly LinkedList<int> _completingOrdersNumbers;
        protected readonly Dictionary<int, int> _completedOrdersInfosByNumber;

        private readonly object _locker;

        protected AbstractTable(IRestaurant restaurant) : base(restaurant)
        {
            _completingOrdersNumbers = new LinkedList<int>();
            _completedOrdersInfosByNumber = new Dictionary<int, int>();

            _locker = new object();

            OrderMarkedCompleted = delegate { };
        }

        public event EventHandler<OrderMarkedCompletedEventArgs> OrderMarkedCompleted;

        public override RestaurantObjectType RestaurantObjectType => RestaurantObjectType.Table;

        protected override void SubscribeToRestaurantObjects()
        {
            Restaurant.TableRemoved += OnTableRemoved;

            Restaurant.CashierAdded += OnCashierAdded;
            Restaurant.CashierRemoved += OnCashierRemoved;

            Restaurant.CookAdded += OnCookAdded;
            Restaurant.CookRemoved += OnCookRemoved;

            Restaurant.ProductDeliveryWindowAdded += OnProductDeliveryWindowAdded;
            Restaurant.ProductDeliveryWindowRemoved += OnProductDeliveryWindowRemoved;

            foreach (var cashier in Restaurant.Cashiers)
                cashier.OrderAccepted += OnOrderAccepted;

            foreach (var cook in Restaurant.Cooks)
                cook.OrderCompleted += OnOrderCompleted;

            foreach (var productDeliveryWindow in Restaurant.ProductDeliveryWindowsByNumber.Values)
                productDeliveryWindow.CompletedOrderTaken += OnCompletedOrderTaken;
        }
        protected override void UnsubscribeFromRestaurantObjects()
        {
            Restaurant.TableRemoved -= OnTableRemoved;

            Restaurant.CashierAdded -= OnCashierAdded;
            Restaurant.CashierRemoved -= OnCashierRemoved;

            Restaurant.CookAdded -= OnCookAdded;
            Restaurant.CookRemoved -= OnCookRemoved;

            Restaurant.ProductDeliveryWindowAdded -= OnProductDeliveryWindowAdded;
            Restaurant.ProductDeliveryWindowRemoved -= OnProductDeliveryWindowRemoved;

            foreach (var cashier in Restaurant.Cashiers)
                cashier.OrderAccepted -= OnOrderAccepted;

            foreach (var cook in Restaurant.Cooks)
                cook.OrderCompleted -= OnOrderCompleted;

            foreach (var productDeliveryWindow in Restaurant.ProductDeliveryWindowsByNumber.Values)
                productDeliveryWindow.CompletedOrderTaken -= OnCompletedOrderTaken;
        }

        protected virtual async void OnOrderAccepted(object sender, OrderAcceptedEventArgs e)
        {
            // Моделирует задержку между принятием заказа и помещением его на информационное табло
            var delayEmulator = Task.Delay(TimeSpan.FromMilliseconds(_random.Next(MIN_DELAY_TIME_IN_MILLISECONDS, MAX_DELAY_TIME_IN_MILLISECONDS))); 

            lock (_locker)
            {
                // TODO: if order number contains - log this 
                if (_completingOrdersNumbers.Contains(e.Order.Number))
                    _completingOrdersNumbers.AddLast(e.Order.Number);
            }

            await delayEmulator;
        }
        protected virtual async void OnCompletedOrderTaken(object sender, CompletedOrderTakenEventArgs e)
        {
            // Моделирует задержку моментом, когда пользователь забрал заказ, и помещением информации на информационное табло
            var delayEmulator = Task.Delay(TimeSpan.FromMilliseconds(_random.Next(MIN_DELAY_TIME_IN_MILLISECONDS, MAX_DELAY_TIME_IN_MILLISECONDS)));

            lock (_locker)
            {
                _completingOrdersNumbers.Remove(e.CompletedOrder.Number); // TODO: if order number contains - log this 

                _completedOrdersInfosByNumber.Remove(e.CompletedOrder.Number);
            }

            await delayEmulator;
        }
        protected virtual async void OnOrderCompleted(object sender, OrderCompletedEventArgs e)
        {
            // Моделирует задержку между моментом, когда заказ приготовлен, и помещением информации на информационное табло
            var delayEmulator = Task.Delay(TimeSpan.FromMilliseconds(_random.Next(MIN_DELAY_TIME_IN_MILLISECONDS, MAX_DELAY_TIME_IN_MILLISECONDS)));

            lock (_locker)
            {
                _completingOrdersNumbers.Remove(e.CompletdOrderInfo.OrderNumber); // TODO: if order number don't contains - log this 

                if (!_completedOrdersInfosByNumber.ContainsKey(e.CompletdOrderInfo.OrderNumber)) // TODO: if order number contains - log this 
                    _completedOrdersInfosByNumber.Add(e.CompletdOrderInfo.OrderNumber, e.CompletdOrderInfo.ProductDeliveryWindowNumber);
            }

            await delayEmulator;

            OrderMarkedCompleted(this, new OrderMarkedCompletedEventArgs(e.CompletdOrderInfo));
        }

        private void OnCashierAdded(object sender, AbstractCashier cashier) => cashier.OrderAccepted += OnOrderAccepted;
        private void OnCashierRemoved(object sender, AbstractCashier cashier) => cashier.OrderAccepted -= OnOrderAccepted;

        private void OnCookAdded(object sender, AbstractCook cook) => cook.OrderCompleted += OnOrderCompleted;
        private void OnCookRemoved(object sender, AbstractCook cook) => cook.OrderCompleted -= OnOrderCompleted;

        private void OnProductDeliveryWindowAdded(object sender, AbstractProductDeliveryWindow productDeliveryWindow) => 
            productDeliveryWindow.CompletedOrderTaken += OnCompletedOrderTaken;
        private void OnProductDeliveryWindowRemoved(object sender, AbstractProductDeliveryWindow productDeliveryWindow) =>
            productDeliveryWindow.CompletedOrderTaken -= OnCompletedOrderTaken;

        private void OnTableRemoved(object sender, AbstractTable abstractTable)
        {
            if (ReferenceEquals(abstractTable, this))
                UnsubscribeFromRestaurantObjects();
        }
    }
}
