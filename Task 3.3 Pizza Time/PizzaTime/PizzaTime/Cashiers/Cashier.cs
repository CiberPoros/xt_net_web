using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PizzaTime.Cooks;
using PizzaTime.Orders;
using PizzaTime.OrdersControllers;
using PizzaTime.Products;

namespace PizzaTime.Cashiers
{
    public class Cashier : ICashier
    {
        public Cashier()
        {
            OrderAccepted = delegate { };
        }

        public event EventHandler<OrderAcceptedEventArgs> OrderAccepted;

        public void AcceptOrder(ICollection<ProductType> productTypes, Action<int> giveOrderNumberCallBack)
        {
            if (productTypes == null)
                throw new ArgumentNullException(nameof(productTypes), "Argument is null.");

            if (!productTypes.Any())
                throw new ArgumentException("Collection must contains at least one element.", nameof(productTypes));

            IOrder order = new Order(productTypes);

            giveOrderNumberCallBack(order.Number);

            OrderAccepted(this, new OrderAcceptedEventArgs(order));

            OrderController.Instance.EnqueueOrder(order);
        }
    }
}
