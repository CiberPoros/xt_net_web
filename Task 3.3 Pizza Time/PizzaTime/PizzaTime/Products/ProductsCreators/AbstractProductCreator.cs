using System;

namespace PizzaTime.Products.ProductsCreators
{
    public abstract class AbstractProductCreator
    {
        public static AbstractProductCreator GetCreator(ProductType productType)
        {
            switch (productType)
            {
                case ProductType.None:
                    throw new ArgumentException("Argument can't have None value.", nameof(productType));
                case ProductType.Pizza:
                    return new PizzaCreator();
                case ProductType.Shawarma:
                    return new ShawarmaCreator();
                default:
                    throw new ArgumentException("Unknown argument type.", nameof(productType));
            }
        }

        public abstract AbstractProduct Create();
    }
}
