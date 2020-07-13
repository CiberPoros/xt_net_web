using System;
using PizzaTime.Restaurants;

namespace PizzaTime.Cashiers
{
    public class Cashier : AbstractCashier
    {
        public Cashier(IRestaurant restaurant, string name) : base(restaurant)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
        }

        public string Name { get; }

        public override string ToString()
        {
            return $"Кассир {Name}";
        }
    }
}
