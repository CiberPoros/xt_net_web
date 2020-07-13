using PizzaTime.OrdersControllers;
using PizzaTime.Restaurants;

namespace Restaurant
{
    public class PizzaRestaurant : AbstractRestaurant
    {
        public PizzaRestaurant(IOrdersController ordersController) : base(ordersController)
        {

        }
    }
}
