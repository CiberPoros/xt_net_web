using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
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
        private AbstractProductDeliveryWindow _nearProductDeliveryWindow;
        private IOrderController _orderController;

        private readonly object _locker;

        private bool _isFree;

        public Cook()
        {
            _isFree = true;

            _locker = new object();

            OrderCompleted = delegate { };
        }

        public event EventHandler<OrderCompletedEventArgs> OrderCompleted;

        public AbstractProductDeliveryWindow NearProductDeliveryWindow
        {
            private get => _nearProductDeliveryWindow;
            set => _nearProductDeliveryWindow = value ?? throw new ArgumentNullException(nameof(value), "Argument is null.");
        }

        public IOrderController OrderController 
        {
            set
            {
                _orderController = value ?? throw new ArgumentNullException(nameof(value), "Argument is null.");
                _orderController.OrderAdded += OnOrderAdded;
            }
        }

        private async void OnOrderAdded(object sender, OrderAddedEventArgs e)
        {
            try
            {
                await TakeNewOrder();
            }
            catch (NullNearDeliveryWindowException)
            {
                // DOTO: log this exception
            }
        }

        private async Task TakeNewOrder()
        {
            if (NearProductDeliveryWindow == null)
            {
                throw new NullNearDeliveryWindowException("Near product delivery window was not set.");
            }

            while (_isFree)
            {
                AbstractOrder order = null;

                lock (_locker)
                {
                    if (!_isFree)
                        return;

                    if (_orderController == null)
                        throw new NullReferenceException($"{nameof(_orderController)} is null.");

                    order = _orderController.DequeueOrder();

                    if (order == null)
                        return;

                    _isFree = false;
                }

                await CompleteOrder(order);
            } 
        }

        private async Task CompleteOrder(AbstractOrder order)
        {
            AbstractCompletedOrder completedOrder = await PreparedOrder(order);

            NearProductDeliveryWindow.AddCompletedOrder(completedOrder);

            OrderCompleted(this, new OrderCompletedEventArgs(new CompletdOrderInfo(order.Number, _nearProductDeliveryWindow.WindowNumber)));

            _isFree = true;
        }

        private async Task<AbstractCompletedOrder> PreparedOrder(AbstractOrder order)
        {
            int allCoocingTime = order.ProductTypes.Sum(productType => AbstractProduct.GetCookintTimeByType(productType));

            var cookingEmulator = Task.Delay(TimeSpan.FromSeconds(allCoocingTime)); // Моделирует приготовление заказа
            LinkedList<AbstractProduct> completedProducts = new LinkedList<AbstractProduct>();
            foreach (var productType in order.ProductTypes)
                completedProducts.AddLast(AbstractProductCreator.GetCreator(productType).Create());

            AbstractCompletedOrder completedOrder = new CompletedOrder(order.Number, completedProducts);
            await cookingEmulator;

            return completedOrder;
        }
    }
}
