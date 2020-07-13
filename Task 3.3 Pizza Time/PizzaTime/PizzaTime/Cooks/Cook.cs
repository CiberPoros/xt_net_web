using PizzaTime.Restaurants;

namespace PizzaTime.Cooks
{
    public class Cook : AbstractCook
    {
        public Cook(IRestaurant restaurant) : base(restaurant)
        {
        }
    }
}
