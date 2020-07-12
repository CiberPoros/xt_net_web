using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PizzaTime.Orders;

namespace PizzaTime.OrdersControllers
{
    public class OrderController : IOrderController
    {
        private readonly Queue<AbstractOrder> _hangingOrders;

        public OrderController()
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
