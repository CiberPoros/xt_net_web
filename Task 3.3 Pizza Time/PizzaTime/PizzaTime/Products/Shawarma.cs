namespace PizzaTime.Products
{
    public class Shawarma : AbstractProduct
    {
        public const int COOKING_TIME = 3;

        public override int CookingTimeInSeconds => COOKING_TIME;

        public override string ToString() => "Shawarma";
    }
}
