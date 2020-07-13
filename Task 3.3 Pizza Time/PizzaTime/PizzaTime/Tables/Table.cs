using PizzaTime.Restaurants;

namespace PizzaTime.Tables
{
    public class Table : AbstractTable
    {
        public Table(IRestaurant restaurant, int number) : base(restaurant)
        {
            Number = number;
        }

        public int Number { get; }

        public override string ToString()
        {
            return $"Табло с номером {Number}";
        }
    }
}
