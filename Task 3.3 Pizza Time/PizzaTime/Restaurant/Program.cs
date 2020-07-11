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

namespace Restaurant
{
    class Program
    {
        static void Main()
        {
            var restaurant = new PizzaRestaurant();

            restaurant.AddCashier(new Cashier());
            restaurant.AddCashier(new Cashier());
            restaurant.AddCashier(new Cashier());
            restaurant.AddCashier(new Cashier());

            restaurant.AddCook(new Cook());
            restaurant.AddCook(new Cook());
            restaurant.AddCook(new Cook());

            restaurant.AddTable(new Table());
            restaurant.AddTable(new Table());

            IClient client = new Client();
            restaurant.GetNearTable().OrderMarkedCompleted += client.OnOrderMarkedCompleted;

            var productTypes = new List<ProductType>
            {
                ProductType.Pizza,
                ProductType.Pizza,
            };

            client.MakeOrder(productTypes, restaurant.GetNearCashier());

            productTypes.Clear();

            productTypes.Add(ProductType.Pizza);
            productTypes.Add(ProductType.Shawarma);

            client.MakeOrder(productTypes, restaurant.GetNearCashier());

            IClient client2 = new Client();
            restaurant.GetNearTable().OrderMarkedCompleted += client2.OnOrderMarkedCompleted;

            var productTypes2 = new List<ProductType>
            {
                ProductType.Shawarma,
                ProductType.Shawarma,
                ProductType.Shawarma,
                ProductType.Shawarma,
            };

            client2.MakeOrder(productTypes2, restaurant.GetNearCashier());

            Console.ReadKey();
        }
    }
}
