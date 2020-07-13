using System.Collections.Generic;
using PizzaTime.Products.ProductsCreators;

namespace PizzaTime.Products
{
    internal static class ProductsCookingTimeInformator
    {
        private readonly static Dictionary<ProductType, AbstractProduct> _instances = new Dictionary<ProductType, AbstractProduct>();

        public static int GetCookingTime(ProductType productType)
        {
            if (_instances.TryGetValue(productType, out AbstractProduct instance))
            {
                return instance.CookingTimeInSeconds;
            }

            instance = AbstractProductCreator.GetCreator(productType).Create();
            _instances.Add(productType, instance);

            return instance.CookingTimeInSeconds;
        }
    }
}
