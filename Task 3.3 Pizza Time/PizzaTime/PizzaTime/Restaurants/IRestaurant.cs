using System;
using System.Collections.Generic;
using PizzaTime.Cashiers;
using PizzaTime.Cooks;
using PizzaTime.OrdersControllers;
using PizzaTime.ProductDeliveryWindows;
using PizzaTime.Tables;

namespace PizzaTime.Restaurants
{
    // Этот интерфейс исключительно для внутренних сущностей ресторана
    public interface IRestaurant
    {
        IReadOnlyCollection<AbstractCashier> Cashiers { get; }
        IReadOnlyCollection<AbstractCook> Cooks { get; }
        IReadOnlyCollection<AbstractTable> Tables { get; }
        IReadOnlyDictionary<int, AbstractProductDeliveryWindow> ProductDeliveryWindowsByNumber { get; }

        event EventHandler<AbstractCashier> CashierAdded;
        event EventHandler<AbstractCashier> CashierRemoved;

        event EventHandler<AbstractCook> CookAdded;
        event EventHandler<AbstractCook> CookRemoved;

        event EventHandler<AbstractTable> TableAdded;
        event EventHandler<AbstractTable> TableRemoved;

        event EventHandler<AbstractProductDeliveryWindow> ProductDeliveryWindowAdded;
        event EventHandler<AbstractProductDeliveryWindow> ProductDeliveryWindowRemoved;

        IOrdersController OrdersController { get; }
    }
}
