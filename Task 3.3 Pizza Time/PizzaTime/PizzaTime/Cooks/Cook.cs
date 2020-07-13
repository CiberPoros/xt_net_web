using System;
using PizzaTime.Restaurants;

namespace PizzaTime.Cooks
{
    public class Cook : AbstractCook
    {
        public Cook(IRestaurant restaurant, string name) : base(restaurant)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
        }

        public string Name { get; }

        public override string ToString()
        {
            return $"Повар {Name}";
        }
    }
}
