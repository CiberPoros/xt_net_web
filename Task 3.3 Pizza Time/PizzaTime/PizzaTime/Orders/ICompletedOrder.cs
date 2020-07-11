using System;
using System.Collections.Generic;
using System.Text;
using PizzaTime.Products;

namespace PizzaTime.Orders
{
    public interface ICompletedOrder
    {
        int Number { get; }

        IReadOnlyCollection<AbstractProduct> Products { get; }
    }
}
