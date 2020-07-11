using System;
using System.Collections.Generic;
using System.Text;
using PizzaTime.Products;

namespace PizzaTime.Orders
{
    public interface IOrder
    {
        int Number { get; }

        IReadOnlyCollection<ProductType> ProductTypes { get; }
    }
}
