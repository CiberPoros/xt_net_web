using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using PizzaTime.Clients;
using PizzaTime.Products;

namespace PizzaTime.Cashiers
{
    public interface ICashier
    {
        event EventHandler<OrderAcceptedEventArgs> OrderAccepted;

        // returns order number
        void AcceptOrder(ICollection<ProductType> productTypes, Action<int> takeOrderNumberCallBack);
    }
}
