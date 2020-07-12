using System;
using System.Collections.Generic;
using System.Text;
using PizzaTime.Restaurants;

namespace PizzaTime.Clients
{

    [Serializable]
    public class ClientIsAlreadyInRestaurantException : Exception
    {
        public ClientIsAlreadyInRestaurantException(IRestaurant currentRestaurant) => CurrentRestaurant = currentRestaurant;
        public ClientIsAlreadyInRestaurantException(IRestaurant currentRestaurant, string message) : base(message) => CurrentRestaurant = currentRestaurant;
        public ClientIsAlreadyInRestaurantException(IRestaurant currentRestaurant, string message, Exception inner) : base(message, inner) => CurrentRestaurant = currentRestaurant;
        protected ClientIsAlreadyInRestaurantException(
          IRestaurant currentRestaurant,
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) => CurrentRestaurant = currentRestaurant;

        public IRestaurant CurrentRestaurant { get; }
    }
}
