namespace PizzaTime.Products.ProductsCreators
{
    public class ShawarmaCreator : AbstractProductCreator
    {
        public override AbstractProduct Create() => new Shawarma();
    }
}
