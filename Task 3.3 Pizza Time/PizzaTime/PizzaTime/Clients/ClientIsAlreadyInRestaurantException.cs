using System;
using PizzaTime.Restaurants;

namespace PizzaTime.Clients
{

    [Serializable]
    public class ClientIsAlreadyInRestaurantException : Exception
    {
        public ClientIsAlreadyInRestaurantException(IRestaurantUI currentRestaurant) => CurrentRestaurant = currentRestaurant;
        public ClientIsAlreadyInRestaurantException(IRestaurantUI currentRestaurant, string message) : base(message) => CurrentRestaurant = currentRestaurant;
        public ClientIsAlreadyInRestaurantException(IRestaurantUI currentRestaurant, string message, Exception inner) : base(message, inner) => CurrentRestaurant = currentRestaurant;
        protected ClientIsAlreadyInRestaurantException(
          IRestaurantUI currentRestaurant,
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) => CurrentRestaurant = currentRestaurant;

        public IRestaurantUI CurrentRestaurant { get; }
    }
}
