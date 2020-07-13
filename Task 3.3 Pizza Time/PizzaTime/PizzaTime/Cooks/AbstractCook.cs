using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PizzaTime.Orders;
using PizzaTime.OrdersControllers;
using PizzaTime.Products;
using PizzaTime.Products.ProductsCreators;
using PizzaTime.Restaurants;

namespace PizzaTime.Cooks
{
    public abstract class AbstractCook : RestaurantObject
    {
        private static readonly Random _random = new Random();

        protected bool _isFree;

        private readonly object _locker;

        protected AbstractCook(IRestaurant restaurant) : base(restaurant)
        {
            _isFree = true;
            _locker = new object();

            OrderCompleted = delegate { };
        }

        public event EventHandler<OrderCompletedEventArgs> OrderCompleted;

        protected override void SubscribeToRestaurantObjects()
        {
            Restaurant.CookRemoved += OnCookRemoved;

            Restaurant.OrdersController.OrderAdded += OnOrderAdded;
        }

        protected override void UnsubscribeFromRestaurantObjects()
        {
            Restaurant.CookRemoved -= OnCookRemoved;

            Restaurant.OrdersController.OrderAdded -= OnOrderAdded;
        }

        protected virtual async void OnOrderAdded(object sender, OrderAddedEventArgs e)
        {
            try
            {
                await TakeNewOrder();
            }
            catch (KeyNotFoundException)
            {
                // DOTO: log this exception
            }
            catch (NullReferenceException)
            {
                // DOTO: log this exception
            }
        }

        private async Task TakeNewOrder()
        {
            if (!Restaurant.ProductDeliveryWindowsByNumber.Any())
            {
                throw new KeyNotFoundException("Restaurant doesn't contain at least one product delivery window.");
            }

            while (_isFree)
            {
                AbstractOrder order = null;

                lock (_locker)
                {
                    if (!_isFree)
                        return;

                    if (Restaurant.OrdersController == null)
                        throw new NullReferenceException($"{nameof(Restaurant.OrdersController)} is null.");

                    order = Restaurant.OrdersController.DequeueOrder();

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

            // Будем считать, что выбирает ближайшее окно.. или наименее загруженное
            var productDeliveryWindow = Restaurant.ProductDeliveryWindowsByNumber.ElementAt(
                _random.Next(Restaurant.ProductDeliveryWindowsByNumber.Count())).Value;

            productDeliveryWindow.AddCompletedOrder(completedOrder);

            OrderCompleted(this, new OrderCompletedEventArgs(new CompletdOrderInfo(order.Number, productDeliveryWindow.WindowNumber)));

            _isFree = true;
        }

        private async Task<AbstractCompletedOrder> PreparedOrder(AbstractOrder order)
        {
            int allCoocingTime = order.ProductTypes.Sum(productType => ProductsCookingTimeInformator.GetCookingTime(productType));

            var cookingEmulator = Task.Delay(TimeSpan.FromSeconds(allCoocingTime)); // Моделирует приготовление заказа
            LinkedList<AbstractProduct> completedProducts = new LinkedList<AbstractProduct>();
            foreach (var productType in order.ProductTypes)
                completedProducts.AddLast(AbstractProductCreator.GetCreator(productType).Create());

            AbstractCompletedOrder completedOrder = new CompletedOrder(order.Number, completedProducts);
            await cookingEmulator;

            return completedOrder;
        }

        private void OnCookRemoved(object sender, AbstractCook cook)
        {
            if (ReferenceEquals(this, cook))
                UnsubscribeFromRestaurantObjects();
        }
    }
}
