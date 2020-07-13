using PizzaTime.Restaurants;

namespace PizzaTime.Cashiers
{
    public class Cashier : AbstractCashier
    {
        public Cashier(IRestaurant restaurant) : base(restaurant)
        {
        }
    }
}
