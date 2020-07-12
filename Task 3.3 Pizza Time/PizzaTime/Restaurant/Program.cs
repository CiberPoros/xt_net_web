using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using PizzaTime.Cashiers;
using PizzaTime.Clients;
using PizzaTime.Cooks;
using PizzaTime.Tables;
using PizzaTime.Products;
using PizzaTime.Restaurants;
using PizzaTime.ProductDeliveryWindows;

namespace Restaurant
{
    class Program
    {
        static void Main()
        {
            IRestaurant restaurant = CreateRestaurant();

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

        private static IRestaurant CreateRestaurant()
        {
            var restaurant = new PizzaRestaurant();

            restaurant.AddProductDeliveryWindow(new ProductDeliveryWindow(1));
            restaurant.AddProductDeliveryWindow(new ProductDeliveryWindow(2));
            restaurant.AddProductDeliveryWindow(new ProductDeliveryWindow(3));

            restaurant.AddCashier(new Cashier());
            restaurant.AddCashier(new Cashier());
            restaurant.AddCashier(new Cashier());
            restaurant.AddCashier(new Cashier());

            restaurant.AddCook(new Cook());
            restaurant.AddCook(new Cook());
            restaurant.AddCook(new Cook());

            restaurant.AddTable(new Table());
            restaurant.AddTable(new Table());

            return restaurant;
        }
    }
}
