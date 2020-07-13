using System;
using System.Collections.Generic;
using PizzaTime.Orders;

namespace PizzaTime.ProductDeliveryWindows
{
    public abstract class AbstractProductDeliveryWindow : IProductDeliveryWindow
    {
        private readonly Dictionary<int, AbstractCompletedOrder> _completedOrders;

        private readonly object _locker = new object();

        public AbstractProductDeliveryWindow(int windowNumber)
        {
            WindowNumber = windowNumber;

            _completedOrders = new Dictionary<int, AbstractCompletedOrder>();

            CompletedOrderTaken = delegate { };
        }

        public event EventHandler<CompletedOrderTakenEventArgs> CompletedOrderTaken;

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
    }
}
