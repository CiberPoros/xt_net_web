using System;
using System.Collections.Generic;
using System.Text;
using PizzaTime.Cashiers;
using PizzaTime.ProductDeliveryWindows;
using PizzaTime.Tables;

namespace PizzaTime.Restaurants
{
    public interface IRestaurant
    {
        // пусть ресторан дает юзеру все кассы, а юзер сам выберет, в какую хочет встать
        IReadOnlyList<ICashier> Cashiers { get; }

        // Табло с информацией о готовящихся/готовых заказах. В ресторане их может быть несколько, но информация во всех идентична. Пусть юзер просто сам выберет, на какой табло подписаться
        IReadOnlyList<ITable> Tables { get; }

        // Окна выдачи готовых заказов. На табло будет отображаться, в каком окне можно забрать заказ
        IReadOnlyDictionary<int, AbstractProductDeliveryWindow> ProductDeliveryWindows { get; }
    }
}
