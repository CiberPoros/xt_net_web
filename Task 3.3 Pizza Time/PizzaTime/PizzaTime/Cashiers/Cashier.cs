using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PizzaTime.Cooks;
using PizzaTime.Orders;
using PizzaTime.OrdersControllers;
using PizzaTime.Products;

namespace PizzaTime.Cashiers
{
    public class Cashier : ICashier
    {
        private IOrderController _orderController;

        public Cashier()
        {
            OrderAccepted = delegate { };
        }

        public IOrderController OrderController { set => _orderController = value ?? throw new ArgumentNullException(nameof(value), "Argument is null."); }

        public event EventHandler<OrderAcceptedEventArgs> OrderAccepted;

        public void AcceptOrder(ICollection<ProductType> productTypes, Action<int> giveOrderNumberCallBack)
        {
            if (productTypes == null)
                throw new ArgumentNullException(nameof(productTypes), "Argument is null.");

            if (!productTypes.Any())
                throw new ArgumentException("Collection must contains at least one element.", nameof(productTypes));

            if (_orderController == null)
                throw new NullReferenceException($"{nameof(_orderController)} is null.");

            AbstractOrder order = new Order(productTypes);

            giveOrderNumberCallBack(order.Number);

            OrderAccepted(this, new OrderAcceptedEventArgs(order));

            _orderController.EnqueueOrder(order);
        }
    }
}
