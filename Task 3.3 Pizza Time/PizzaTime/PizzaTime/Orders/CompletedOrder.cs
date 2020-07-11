using System;
using System.Collections.Generic;
using System.Text;
using PizzaTime.Products;

namespace PizzaTime.Orders
{
    public class CompletedOrder : ICompletedOrder
    {
        protected readonly LinkedList<AbstractProduct> _products;

        public CompletedOrder(int number, ICollection<AbstractProduct> products)
        {
            _products = new LinkedList<AbstractProduct>(products);
            Number = number;
        }

        public int Number { get; }

        public IReadOnlyCollection<AbstractProduct> Products => _products;

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.Append($"Полученный заказ # {Number}. Составляющие: ");
            stringBuilder.Append(Environment.NewLine);

            foreach (var product in _products)
            {
                stringBuilder.Append(product);
                stringBuilder.Append(Environment.NewLine);
            }

            stringBuilder.Append(Environment.NewLine);

            return stringBuilder.ToString();
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            if (!(obj is CompletedOrder other))
                return false;

            return Number == other.Number;
        }

        public override int GetHashCode() => Number;
    }
}
