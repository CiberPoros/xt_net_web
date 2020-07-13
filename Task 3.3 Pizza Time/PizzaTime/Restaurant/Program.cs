using System;
using System.Collections.Generic;
using PizzaTime.Cashiers;
using PizzaTime.Clients;
using PizzaTime.Cooks;
using PizzaTime.Tables;
using PizzaTime.Products;
using PizzaTime.Restaurants;
using PizzaTime.ProductDeliveryWindows;
using PizzaTime.OrdersControllers;

namespace Restaurant
{
    class Program
    {
        static void Main()
        {
            IRestaurantUI restaurant = CreateRestaurant();

            IClient client = new Client();
            client.EnterRestaurant(restaurant);

            var productTypes = new List<ProductType>
            {
                ProductType.Pizza,
                ProductType.Pizza,
                ProductType.Shawarma,
                ProductType.Shawarma,
            };
            client.MakeOrder(productTypes);

            productTypes.Clear();

            productTypes.Add(ProductType.Pizza);
            productTypes.Add(ProductType.Shawarma);
            client.MakeOrder(productTypes);

            IClient client2 = new Client();
            client2.EnterRestaurant(restaurant);

            var productTypes2 = new List<ProductType>
            {
                ProductType.Shawarma,
                ProductType.Shawarma,
                ProductType.Shawarma,
                ProductType.Shawarma,
            };

            client2.MakeOrder(productTypes2);

            Console.ReadKey();
        }

        private static IRestaurantUI CreateRestaurant()
        {
            var restaurant = new PizzaRestaurant(new OrdersController());

            restaurant.AddProductDeliveryWindow(new ProductDeliveryWindow(1));
            restaurant.AddProductDeliveryWindow(new ProductDeliveryWindow(2));
            restaurant.AddProductDeliveryWindow(new ProductDeliveryWindow(3));

            restaurant.AddCashier(new Cashier(restaurant));
            restaurant.AddCashier(new Cashier(restaurant));
            restaurant.AddCashier(new Cashier(restaurant));
            restaurant.AddCashier(new Cashier(restaurant));

            restaurant.AddCook(new Cook(restaurant));
            restaurant.AddCook(new Cook(restaurant));
            restaurant.AddCook(new Cook(restaurant));

            restaurant.AddTable(new Table(restaurant));
            restaurant.AddTable(new Table(restaurant));

            return restaurant;
        }
    }
}
