using System;
using System.Collections.Generic;
using System.Linq;
using PizzaTime.Orders;
using PizzaTime.Products;
using PizzaTime.Restaurants;

namespace PizzaTime.Cashiers
{
    public abstract class AbstractCashier : RestaurantObject
    {
        public AbstractCashier(IRestaurant restaurant) : base(restaurant) 
        {
            OrderAccepted = delegate { };
        }

        public event EventHandler<OrderAcceptedEventArgs> OrderAccepted;

        public override RestaurantObjectType RestaurantObjectType => RestaurantObjectType.Cashier; 

        public virtual void AcceptOrder(ICollection<ProductType> productTypes, Action<int> takeOrderNumberCallBack)
        {
            if (productTypes == null)
                throw new ArgumentNullException(nameof(productTypes), "Argument is null.");

            if (!productTypes.Any())
                throw new ArgumentException("Collection must contains at least one element.", nameof(productTypes));

            if (Restaurant.OrdersController == null)
                throw new NullReferenceException($"{nameof(Restaurant.OrdersController)} is null.");

            AbstractOrder order = new Order(productTypes);

            takeOrderNumberCallBack(order.Number);

            OrderAccepted(this, new OrderAcceptedEventArgs(order));

            Restaurant.OrdersController.EnqueueOrder(order);
        }

        protected override void SubscribeToRestaurantObjects() { }

        protected override void UnsubscribeFromRestaurantObjects() { }
    }
}
