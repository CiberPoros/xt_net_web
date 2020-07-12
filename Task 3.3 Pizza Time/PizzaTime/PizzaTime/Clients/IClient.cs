using System;
using System.Collections.Generic;
using System.Text;
using PizzaTime.Cashiers;
using PizzaTime.Orders;
using PizzaTime.ProductDeliveryWindows;
using PizzaTime.Products;
using PizzaTime.Restaurants;
using PizzaTime.Tables;

namespace PizzaTime.Clients
{
    public interface IClient
    {
        void EnterRestaurant(IRestaurant restaurant);
        bool LeaveRestaurant();

        // returns order number
        void MakeOrder(ICollection<ProductType> productTypes);
    }
}
