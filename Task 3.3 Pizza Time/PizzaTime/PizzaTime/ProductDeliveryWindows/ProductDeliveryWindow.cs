using PizzaTime.Restaurants;

namespace PizzaTime.ProductDeliveryWindows
{
    public class ProductDeliveryWindow : AbstractProductDeliveryWindow
    {
        public ProductDeliveryWindow(IRestaurant restaurant, int windowNumber) : base (restaurant, windowNumber)
        {

        }
    }
}
