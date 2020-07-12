using System;
using System.Collections.Generic;
using System.Text;
using PizzaTime.Cashiers;
using PizzaTime.Clients;
using PizzaTime.Cooks;
using PizzaTime.ProductDeliveryWindows;

namespace PizzaTime.Tables
{
    public class Table : ITable
    {
        private readonly LinkedList<int> _completingOrdersNumbers;
        private readonly Dictionary<int, int> _completedOrdersInfosByNumber;

        private readonly object _locker;

        public Table()
        {
            _completingOrdersNumbers = new LinkedList<int>();
            _completedOrdersInfosByNumber = new Dictionary<int, int>();

            _locker = new object();

            OrderMarkedCompleted = delegate { };
        }

        public event EventHandler<OrderMarkedCompletedEventArgs> OrderMarkedCompleted;

        public void OnOrderAccepted(object sender, OrderAcceptedEventArgs e)
        {
            lock (_locker)
            {
                // TODO: if order number contains - log this 
                if (_completingOrdersNumbers.Contains(e.Order.Number))
                    _completingOrdersNumbers.AddLast(e.Order.Number);
            }
        }

        public void OnCompletedOrderTaken(object sender, CompletedOrderTakenEventArgs e)
        {
            lock (_locker)
            {
                _completingOrdersNumbers.Remove(e.CompletedOrder.Number); // TODO: if order number contains - log this 

                _completedOrdersInfosByNumber.Remove(e.CompletedOrder.Number);
            }
        }

        public void OnOrderCompleted(object sender, OrderCompletedEventArgs e)
        {
            lock (_locker)
            {
                _completingOrdersNumbers.Remove(e.CompletdOrderInfo.OrderNumber); // TODO: if order number don't contains - log this 

                if (!_completedOrdersInfosByNumber.ContainsKey(e.CompletdOrderInfo.OrderNumber)) // TODO: if order number contains - log this 
                    _completedOrdersInfosByNumber.Add(e.CompletdOrderInfo.OrderNumber, e.CompletdOrderInfo.ProductDeliveryWindowNumber);
            }

            OrderMarkedCompleted(this, new OrderMarkedCompletedEventArgs(e.CompletdOrderInfo));
        }
    }
}
