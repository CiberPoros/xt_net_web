using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PizzaTime.Orders;
using PizzaTime.OrdersControllers;
using PizzaTime.ProductDeliveryWindows;
using PizzaTime.Products;
using PizzaTime.Products.ProductsCreators;

namespace PizzaTime.Cooks
{
    public class Cook : ICook
    {
        private bool _isFree;

        public Cook()
        {
            _isFree = true;
            OrderCompleted = delegate { };

            OrderCompleted += OnOrderCompleted;
        }

        ~Cook()
        {
            OrderCompleted -= OnOrderCompleted;
        }

        public event EventHandler<OrderCompletedEventArgs> OrderCompleted;

        public void OnOrderAdded(object sender, OrderAddedEventArgs e) => TakeNewOrder();

        private void OnOrderCompleted(object sender, OrderCompletedEventArgs e) => TakeNewOrder();

        private void TakeNewOrder()
        {
            if (!_isFree)
                return;

            IOrder order = OrderController.Instance.DequeueOrder();

            if (order == null)
                return;

            CompleteOrder(order);
        }

        private void CompleteOrder(IOrder order)
        {
            _isFree = false;

            int allCoocingTime = order.ProductTypes.Sum(productType => AbstractProduct.GetCookintTimeByType(productType));

            LinkedList<AbstractProduct> completedProducts = new LinkedList<AbstractProduct>();
            foreach (var productType in order.ProductTypes)
                completedProducts.AddLast(AbstractProductCreator.GetCreator(productType).Create());

            ICompletedOrder completedOrder = new CompletedOrder(order.Number, completedProducts);

            ProductDeliveryWindow.Instance.AddCompletedOrder(completedOrder);

            _isFree = true;

            OrderCompleted(this, new OrderCompletedEventArgs(completedOrder));
        }
    }
}
