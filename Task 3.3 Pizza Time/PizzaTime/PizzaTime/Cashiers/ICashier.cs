using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using PizzaTime.Clients;
using PizzaTime.OrdersControllers;
using PizzaTime.Products;

namespace PizzaTime.Cashiers
{
    public interface ICashier
    {
        event EventHandler<OrderAcceptedEventArgs> OrderAccepted;

        IOrderController OrderController { set; }

        // returns order number
        void AcceptOrder(ICollection<ProductType> productTypes, Action<int> takeOrderNumberCallBack);
    }
}
