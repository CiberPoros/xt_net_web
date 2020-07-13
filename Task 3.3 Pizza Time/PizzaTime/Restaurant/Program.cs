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
using Logger;

namespace Restaurant
{
    class Program
    {
        static void Main()
        {
            IRestaurantUI restaurant = CreateRestaurant(out ILogger logger);

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

        private static void OnLogged(object sender, string logInfo)
        {
            Console.WriteLine(logInfo);
            Console.WriteLine();
        }

        private static IRestaurantUI CreateRestaurant(out ILogger logger)
        {
            var restaurant = new PizzaRestaurant(new OrdersController(), "Donna Pizza");

            logger = new RestaurantActionsLogger(restaurant);
            logger.Logged += OnLogged;

            restaurant.AddProductDeliveryWindow(new ProductDeliveryWindow(restaurant, 1));
            restaurant.AddProductDeliveryWindow(new ProductDeliveryWindow(restaurant, 2));
            restaurant.AddProductDeliveryWindow(new ProductDeliveryWindow(restaurant, 3));

            restaurant.AddCashier(new Cashier(restaurant, "Вася"));
            restaurant.AddCashier(new Cashier(restaurant, "Петя"));
            restaurant.AddCashier(new Cashier(restaurant, "Даша"));
            restaurant.AddCashier(new Cashier(restaurant, "Саша"));

            restaurant.AddCook(new Cook(restaurant, "Бурген"));
            restaurant.AddCook(new Cook(restaurant, "Омар"));
            restaurant.AddCook(new Cook(restaurant, "Альберто"));

            restaurant.AddTable(new Table(restaurant, 1703));
            restaurant.AddTable(new Table(restaurant, 1428));

            return restaurant;
        }
    }
}
