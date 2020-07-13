using System;
using System.Collections.Generic;
using System.Text;

namespace PizzaTime
{
    public enum RestaurantObjectType : byte
    {
        None = 0,
        Cashier = 1,
        Cook = 2, 
        ProductDeliveryWindow = 3,
        Table = 4,
    }
}
