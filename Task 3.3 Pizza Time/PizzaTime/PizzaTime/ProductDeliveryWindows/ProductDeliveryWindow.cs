using System;
using System.Collections.Generic;
using System.Text;
using PizzaTime.Orders;

namespace PizzaTime.ProductDeliveryWindows
{
    public class ProductDeliveryWindow : AbstractProductDeliveryWindow
    {
        private readonly Dictionary<int, AbstractCompletedOrder> _completedOrders;

        private readonly object _locker = new object();

        public ProductDeliveryWindow(int windowNumber) : base (windowNumber)
        {
            _completedOrders = new Dictionary<int, AbstractCompletedOrder>();

            CompletedOrderTaken = delegate { };
        }

        public override event EventHandler<CompletedOrderTakenEventArgs> CompletedOrderTaken;

        public override void AddCompletedOrder(AbstractCompletedOrder completedOrder)
        {
            if (completedOrder == null)
                throw new ArgumentNullException(nameof(completedOrder), "Parameter is null.");

            lock (_locker)
            {
                _completedOrders.Add(completedOrder.Number, completedOrder);
            }
        }

        public override AbstractCompletedOrder ExtractCompletedOrderByNumber(int orderNumber)
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
