using System;
using PizzaTime.Cashiers;
using PizzaTime.Clients;
using PizzaTime.Cooks;
using PizzaTime.Tables;
using PizzaTime.Products;
using PizzaTime.Restaurants;
using PizzaTime.ProductDeliveryWindows;
using PizzaTime.OrdersControllers;
using Logger;
using System.Linq;

namespace Restaurant
{
    class Program
    {
        static void Main()
        {
            IRestaurantUI restaurant = CreateRestaurant(out ILogger logger);

            IClient client1 = new Client();
            client1.EnterRestaurant(restaurant);

            IClient client2 = new Client();
            client2.EnterRestaurant(restaurant);

            IClient client3 = new Client();
            client3.EnterRestaurant(restaurant);

            MakeOrder(client1, ProductType.Pizza);
            MakeOrder(client2, ProductType.Pizza, ProductType.Shawarma);
            MakeOrder(client1, ProductType.Pizza);
            MakeOrder(client3, ProductType.Shawarma, ProductType.Shawarma, ProductType.Shawarma);
            MakeOrder(client1, ProductType.Shawarma);

            Console.ReadKey();
        }

        private static void MakeOrder(IClient client, params ProductType[] productTypes) =>
            client.MakeOrder(productTypes.ToList());


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
