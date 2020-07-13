using PizzaTime.Orders;

namespace PizzaTime.ProductDeliveryWindows
{
    // interface for clients
    public interface IProductDeliveryWindow
    {
        AbstractCompletedOrder ExtractCompletedOrderByNumber(int orderNumber);
    }
}
