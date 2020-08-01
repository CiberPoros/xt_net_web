using System.Collections.Generic;
using PizzaTime.ProductDeliveryWindows;
using PizzaTime.Products;
using PizzaTime.Restaurants;
using PizzaTime.Tables;

namespace PizzaTime.Clients
{
    public interface IClient
    {
        void EnterRestaurant(IRestaurantUI restaurant);
        bool LeaveRestaurant();

        void MakeOrder(ICollection<ProductType> productTypes);
        void TakeCompletedOrder(int orderNumber, IProductDeliveryWindow productDeliveryWindow);
    }
}
