using System.Collections.Generic;
using PizzaTime.Products;

namespace PizzaTime.Orders
{
    public class Order : AbstractOrder
    {
        private static int _lastNumber;

        protected readonly LinkedList<ProductType> _productTypes;

        static Order()
        {
            _lastNumber = 1;
        }

        public Order(ICollection<ProductType> productTypes)
        {
            _productTypes = new LinkedList<ProductType>(productTypes);
            Number = _lastNumber++;
        }

        public override IReadOnlyCollection<ProductType> ProductTypes => _productTypes;
    }
}
