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

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.Append($"Номер заказа: {Number}; Состав заказа: {Environment.NewLine}");
            foreach (ProductType productType in ProductTypes)
                stringBuilder.Append($"{productType}{Environment.NewLine}");

            return stringBuilder.ToString();
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            if (!(obj is Order other))
                return false;

            return Number == other.Number;
        }

        public override int GetHashCode() => Number;
    }
}
