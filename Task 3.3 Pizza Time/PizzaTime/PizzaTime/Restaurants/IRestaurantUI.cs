using System.Collections.Generic;
using PizzaTime.Cashiers;
using PizzaTime.ProductDeliveryWindows;
using PizzaTime.Tables;

namespace PizzaTime.Restaurants
{
    // Этот интерфейс для клиентов
    public interface IRestaurantUI
    {
        // пусть ресторан дает юзеру все кассы, а юзер сам выберет, в какую хочет встать
        IReadOnlyCollection<AbstractCashier> Cashiers { get; }

        // Табло с информацией о готовящихся/готовых заказах. В ресторане их может быть несколько, но информация во всех идентична. Пусть юзер просто сам выберет, на какой табло подписаться
        IReadOnlyCollection<AbstractTable> Tables { get; }
 
        // На табло будет отображаться, в каком окне можно забрать заказ
        IProductDeliveryWindow GetProductDeliveryWindowByNumber(int number);
    }
}
