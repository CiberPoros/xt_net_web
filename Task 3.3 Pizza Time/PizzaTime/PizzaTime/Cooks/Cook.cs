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

        private readonly object _locker;

        public Cook()
        {
            _isFree = true;

            _locker = new object();

            OrderCompleted = delegate { };

            OrderCompleted += OnOrderCompleted;
        }

        ~Cook()
        {
            OrderCompleted -= OnOrderCompleted;
        }

        public event EventHandler<OrderCompletedEventArgs> OrderCompleted;

        public async void OnOrderAdded(object sender, OrderAddedEventArgs e) => await TakeNewOrder();

        private async void OnOrderCompleted(object sender, OrderCompletedEventArgs e) => await TakeNewOrder();

        private async Task TakeNewOrder()
        {
            IOrder order = null;

            lock (_locker)
            {
                if (!_isFree)
                    return;

                order = OrderController.Instance.DequeueOrder();

                if (order == null)
                    return;

                _isFree = false; 
            }

            await CompleteOrder(order);
        }

        private async Task CompleteOrder(IOrder order)
        {
            int allCoocingTime = order.ProductTypes.Sum(productType => AbstractProduct.GetCookintTimeByType(productType));

            var awaiter = Task.Run(async delegate
            {
                await Task.Delay(TimeSpan.FromSeconds(allCoocingTime));
            });

            LinkedList<AbstractProduct> completedProducts = new LinkedList<AbstractProduct>();
            foreach (var productType in order.ProductTypes)
                completedProducts.AddLast(AbstractProductCreator.GetCreator(productType).Create());

            ICompletedOrder completedOrder = new CompletedOrder(order.Number, completedProducts);

            ProductDeliveryWindow.Instance.AddCompletedOrder(completedOrder);

            await awaiter;

            _isFree = true;

            OrderCompleted(this, new OrderCompletedEventArgs(completedOrder));
        }
    }
}
