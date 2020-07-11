using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PizzaTime.Orders;

namespace PizzaTime.OrdersControllers
{
    public class OrderController : IOrderController
    {
        private static readonly Lazy<IOrderController> _instance = new Lazy<IOrderController>(() => new OrderController());

        private readonly Queue<IOrder> _hangingOrders;

        private OrderController()
        {
            _hangingOrders = new Queue<IOrder>();

            OrderAdded = delegate { };
        }

        public event EventHandler<OrderAddedEventArgs> OrderAdded;

        public static IOrderController Instance => _instance.Value;

        public void EnqueueOrder(IOrder order)
        {
            if (order == null)
                throw new ArgumentNullException(nameof(order), "Parameter is null.");

            _hangingOrders.Enqueue(order);

            OrderAdded(this, new OrderAddedEventArgs(order));
        }

        public IOrder DequeueOrder() => _hangingOrders.Any() ? _hangingOrders.Dequeue() : null;
    }
}
