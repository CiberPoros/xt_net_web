using System;
using System.Collections.Generic;
using System.Text;

namespace PizzaTime.Products
{
    public abstract class AbstractProduct
    {
        // Подумать, как это можно заменить...
        public static int GetCookintTimeByType(ProductType productType)
        {
            switch (productType)
            {
                case ProductType.None:
                    throw new ArgumentException("Product type can't be None.", nameof(productType));
                case ProductType.Pizza:
                    return Pizza.COOKING_TIME;
                case ProductType.Shawarma:
                    return Shawarma.COOKING_TIME;
                default:
                    throw new ArgumentException("Unknown product type.", nameof(productType));
            }
        }

        public abstract int CookingTimeInSeconds { get; }
    }
}
