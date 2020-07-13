namespace PizzaTime.Products.ProductsCreators
{
    public class PizzaCreator : AbstractProductCreator
    {
        public override AbstractProduct Create() => new Pizza();
    }
}
