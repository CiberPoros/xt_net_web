using System;
using System.Collections.Generic;
using System.Text;
using PizzaTime.Products;

namespace PizzaTime.Orders
{
    public abstract class AbstractOrder
    {
        public int Number { get; protected set; }

        public abstract IReadOnlyCollection<ProductType> ProductTypes { get; }
    }
}
