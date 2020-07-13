using System;
using System.Collections.Generic;
using PizzaTime.Orders;
using PizzaTime.Restaurants;

namespace PizzaTime.ProductDeliveryWindows
{
    public abstract class AbstractProductDeliveryWindow : RestaurantObject, IProductDeliveryWindow
    {
        private readonly Dictionary<int, AbstractCompletedOrder> _completedOrders;

        private readonly object _locker = new object();

        public AbstractProductDeliveryWindow(IRestaurant restaurant, int windowNumber) : base(restaurant)
        {
            WindowNumber = windowNumber;

            _completedOrders = new Dictionary<int, AbstractCompletedOrder>();

            CompletedOrderTaken = delegate { };
        }

        public event EventHandler<CompletedOrderTakenEventArgs> CompletedOrderTaken;

        public override RestaurantObjectType RestaurantObjectType => RestaurantObjectType.ProductDeliveryWindow;

        public int WindowNumber { get; }

        public virtual void AddCompletedOrder(AbstractCompletedOrder completedOrder)
        {
            if (completedOrder == null)
                throw new ArgumentNullException(nameof(completedOrder), "Parameter is null.");

            lock (_locker)
            {
                _completedOrders.Add(completedOrder.Number, completedOrder);
            }
        }

        public virtual AbstractCompletedOrder ExtractCompletedOrderByNumber(int orderNumber)
        {
            AbstractCompletedOrder completedOrder = null;

            lock (_locker)
            {
                if (!_completedOrders.TryGetValue(orderNumber, out completedOrder))
                {
                    return null; // Стоит ли тут бросать экзепшн, если такого заказа нет?
                }
            }

            CompletedOrderTaken(this, new CompletedOrderTakenEventArgs(completedOrder));

            return completedOrder;
        }

        protected override void SubscribeToRestaurantObjects() { }

        protected override void UnsubscribeFromRestaurantObjects() { }

        public override string ToString()
        {
            return $"Окно выдачи с номером {WindowNumber}";
        }
    }
}
