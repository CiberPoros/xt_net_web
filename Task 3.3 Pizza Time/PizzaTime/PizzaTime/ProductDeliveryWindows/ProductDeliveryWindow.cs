using System;
using System.Collections.Generic;
using System.Text;
using PizzaTime.Orders;

namespace PizzaTime.ProductDeliveryWindows
{
    public class ProductDeliveryWindow : IProductDeliveryWindow
    {
        private static readonly Lazy<IProductDeliveryWindow> _instance = new Lazy<IProductDeliveryWindow>(() => new ProductDeliveryWindow());

        private readonly Dictionary<int, ICompletedOrder> _completedOrders;

        private readonly object _locker = new object();

        private ProductDeliveryWindow()
        {
            _completedOrders = new Dictionary<int, ICompletedOrder>();
        }

        public static IProductDeliveryWindow Instance => _instance.Value;

        public event EventHandler<CompletedOrderTakenEventArgs> CompletedOrderTaken;

        public void AddCompletedOrder(ICompletedOrder completedOrder)
        {
            if (completedOrder == null)
                throw new ArgumentNullException(nameof(completedOrder), "Parameter is null.");

            lock (_locker)
            {
                _completedOrders.Add(completedOrder.Number, completedOrder);
            }
        }

        public ICompletedOrder ExtractCompletedOrderByNumber(int orderNumber)
        {
            ICompletedOrder completedOrder = null;

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
