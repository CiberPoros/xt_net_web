using System;
using System.Collections.Generic;
using System.Linq;
using PizzaTime.Orders;

namespace PizzaTime.OrdersControllers
{
    public class OrdersController : IOrdersController
    {
        private readonly Queue<AbstractOrder> _hangingOrders;

        public OrdersController()
        {
            _hangingOrders = new Queue<AbstractOrder>();

            OrderAdded = delegate { };
        }

        public event EventHandler<OrderAddedEventArgs> OrderAdded;

        public void EnqueueOrder(AbstractOrder order)
        {
            if (order == null)
                throw new ArgumentNullException(nameof(order), "Parameter is null.");

            _hangingOrders.Enqueue(order);

            OrderAdded(this, new OrderAddedEventArgs(order));
        }

        public AbstractOrder DequeueOrder() => _hangingOrders.Any() ? _hangingOrders.Dequeue() : null;
    }
}
