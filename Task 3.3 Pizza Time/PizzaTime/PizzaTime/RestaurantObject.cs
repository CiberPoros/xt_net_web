using System;
using PizzaTime.Restaurants;

namespace PizzaTime
{
    public abstract class RestaurantObject
    {
        private IRestaurant _restaurant;

        public RestaurantObject(IRestaurant restaurant)
        {
            _restaurant = restaurant;

            SubscribeToRestaurantObjects();
        }

        public IRestaurant Restaurant 
        { 
            protected get => _restaurant; 
            set => _restaurant = value ?? throw new ArgumentNullException(nameof(value), "Argument is null."); 
        }

        protected abstract void SubscribeToRestaurantObjects();
        protected abstract void UnsubscribeFromRestaurantObjects();
    }
}
