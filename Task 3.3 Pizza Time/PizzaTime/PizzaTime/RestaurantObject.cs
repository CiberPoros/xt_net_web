using System;
using PizzaTime.Restaurants;

namespace PizzaTime
{
    public abstract class RestaurantObject
    {
        private IRestaurant _restaurant;

        public RestaurantObject(IRestaurant restaurant)
        {
            _restaurant = restaurant ?? throw new ArgumentNullException(nameof(restaurant), "Argument is null.");

            SubscribeToRestaurantObjects();
        }

        public IRestaurant Restaurant 
        { 
            protected get => _restaurant; 
            set => _restaurant = value ?? throw new ArgumentNullException(nameof(value), "Argument is null."); 
        }

        public abstract RestaurantObjectType RestaurantObjectType { get; }

        protected abstract void SubscribeToRestaurantObjects();
        protected abstract void UnsubscribeFromRestaurantObjects();
    }
}
