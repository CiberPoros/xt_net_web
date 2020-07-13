using System;
using PizzaTime;
using PizzaTime.Cashiers;
using PizzaTime.Cooks;
using PizzaTime.ProductDeliveryWindows;
using PizzaTime.Restaurants;
using PizzaTime.Tables;

namespace Logger
{
    public class RestaurantActionsLogger : ILogger
    {
        private readonly IRestaurant _restaurant;

        public RestaurantActionsLogger(IRestaurant restaurant)
        {
            _restaurant = restaurant ?? throw new ArgumentNullException(nameof(IRestaurant), "Argument is null.");

            Logged = delegate { };

            Subscribe();
        }

        public event EventHandler<string> Logged;

        public void OnRestaurantObjectAdded(object sender, RestaurantObject restaurantObject)
        {
            Logged(this, $"В ресторан {sender} добавлен объект типа {restaurantObject.GetType()}. " +
                $"Иформация по объекту:{Environment.NewLine}{restaurantObject}");

            switch (restaurantObject.RestaurantObjectType)
            {
                case RestaurantObjectType.None:
                    throw new ArgumentException("Unknown restaurant object.", nameof(restaurantObject));
                case RestaurantObjectType.Cashier:
                    (restaurantObject as AbstractCashier).OrderAccepted += OnOrderAccepted;
                    break;
                case RestaurantObjectType.Cook:
                    (restaurantObject as AbstractCook).OrderCompleted += OnOrderCompleted;
                    break;
                case RestaurantObjectType.ProductDeliveryWindow:
                    (restaurantObject as AbstractProductDeliveryWindow).CompletedOrderTaken += OnCompletedOrderTaken;
                    break;
                case RestaurantObjectType.Table:
                    (restaurantObject as AbstractTable).OrderMarkedCompleted += OnOrderMarkedCompleted;
                    break;
                default:
                    throw new ArgumentException("Unknown restaurant object type.", nameof(restaurantObject.RestaurantObjectType)); ;
            }
        }

        public void OnRestaurantObjectRemoved(object sender, RestaurantObject restaurantObject)
        {
            Logged(this, $"Из ресторана {sender} удален объект типа {restaurantObject.GetType()}. " +
                $"Иформация по объекту:{Environment.NewLine}{restaurantObject}");

            switch (restaurantObject.RestaurantObjectType)
            {
                case RestaurantObjectType.None:
                    throw new ArgumentException("Unknown restaurant object.", nameof(restaurantObject));
                case RestaurantObjectType.Cashier:
                    (restaurantObject as AbstractCashier).OrderAccepted -= OnOrderAccepted;
                    break;
                case RestaurantObjectType.Cook:
                    (restaurantObject as AbstractCook).OrderCompleted -= OnOrderCompleted;
                    break;
                case RestaurantObjectType.ProductDeliveryWindow:
                    (restaurantObject as AbstractProductDeliveryWindow).CompletedOrderTaken -= OnCompletedOrderTaken;
                    break;
                case RestaurantObjectType.Table:
                    (restaurantObject as AbstractTable).OrderMarkedCompleted -= OnOrderMarkedCompleted;
                    break;
                default:
                    throw new ArgumentException("Unknown restaurant object type.", nameof(restaurantObject.RestaurantObjectType));
            }
        }

        public void OnOrderAccepted(object sender, OrderAcceptedEventArgs e)
        {
            Logged(this, $"Кассир \"{sender}\" принял заказ у клиента. Информация по заказу:{Environment.NewLine}{e.Order}");
        }

        public void OnOrderCompleted(object sender, OrderCompletedEventArgs e)
        {
            Logged(this, $"Повар \"{sender}\" приготовил заказ с номером {e.CompletdOrderInfo.OrderNumber}.{Environment.NewLine}" +
                $"Зазаз был направлен в окно выдачи с номером {e.CompletdOrderInfo.ProductDeliveryWindowNumber}.");
        }

        public void OnOrderMarkedCompleted(object sender, OrderMarkedCompletedEventArgs e)
        {
            Logged(this, $"На табло \"{sender}\" заказ с номером {e.CompletdOrderInfo.OrderNumber} " +
                $"перешел в группу готовых заказов. Окно выдачи: {e.CompletdOrderInfo.ProductDeliveryWindowNumber}.");
        }

        public void OnCompletedOrderTaken(object sender, CompletedOrderTakenEventArgs e)
        {
            Logged(this, $"Из окна выдачи с номером {(sender as AbstractProductDeliveryWindow).WindowNumber} забран заказ с номером {e.CompletedOrder.Number}.");
        }

        public void Dispose()
        {
            Unsubscribe();
        }

        private void Subscribe()
        {
            _restaurant.CashierAdded += OnRestaurantObjectAdded;
            _restaurant.CashierRemoved += OnRestaurantObjectRemoved;

            _restaurant.CookAdded += OnRestaurantObjectAdded;
            _restaurant.CookRemoved += OnRestaurantObjectRemoved;

            _restaurant.ProductDeliveryWindowAdded += OnRestaurantObjectAdded;
            _restaurant.ProductDeliveryWindowRemoved += OnRestaurantObjectRemoved;

            _restaurant.TableAdded += OnRestaurantObjectAdded;
            _restaurant.TableRemoved += OnRestaurantObjectRemoved;

            foreach (var cashier in _restaurant.Cashiers)
                cashier.OrderAccepted += OnOrderAccepted;

            foreach (var cook in _restaurant.Cooks)
                cook.OrderCompleted += OnOrderCompleted;

            foreach (var table in _restaurant.Tables)
                table.OrderMarkedCompleted += OnOrderMarkedCompleted;

            foreach (var productDeliveryWindow in _restaurant.ProductDeliveryWindowsByNumber.Values)
                productDeliveryWindow.CompletedOrderTaken += OnCompletedOrderTaken;
        }

        private void Unsubscribe()
        {
            _restaurant.CashierAdded -= OnRestaurantObjectAdded;
            _restaurant.CashierRemoved -= OnRestaurantObjectRemoved;

            _restaurant.CookAdded -= OnRestaurantObjectAdded;
            _restaurant.CookRemoved -= OnRestaurantObjectRemoved;

            _restaurant.ProductDeliveryWindowAdded -= OnRestaurantObjectAdded;
            _restaurant.ProductDeliveryWindowRemoved -= OnRestaurantObjectRemoved;

            _restaurant.TableAdded -= OnRestaurantObjectAdded;
            _restaurant.TableRemoved -= OnRestaurantObjectRemoved;

            foreach (var cashier in _restaurant.Cashiers)
                cashier.OrderAccepted -= OnOrderAccepted;

            foreach (var cook in _restaurant.Cooks)
                cook.OrderCompleted -= OnOrderCompleted;

            foreach (var table in _restaurant.Tables)
                table.OrderMarkedCompleted -= OnOrderMarkedCompleted;

            foreach (var productDeliveryWindow in _restaurant.ProductDeliveryWindowsByNumber.Values)
                productDeliveryWindow.CompletedOrderTaken -= OnCompletedOrderTaken;
        }
    }
}
