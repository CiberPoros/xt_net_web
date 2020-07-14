namespace PizzaTime.Products
{
    public class Pizza : AbstractProduct
    {
        public const int COOKING_TIME = 2;

        public override int CookingTimeInSeconds => COOKING_TIME;

        public override string ToString() => "Pizza";
    }
}
