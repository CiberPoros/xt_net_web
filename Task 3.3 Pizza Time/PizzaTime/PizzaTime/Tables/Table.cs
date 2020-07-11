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
        private readonly LinkedList<int> _completedOrdersNumbers;

        private readonly object _locker;

        public Table()
        {
            _completingOrdersNumbers = new LinkedList<int>();
            _completedOrdersNumbers = new LinkedList<int>();

            _locker = new object();

            OrderMarkedCompleted = delegate { };
        }

        public event EventHandler<OrderMarkedCompletedEventArgs> OrderMarkedCompleted;

        public IReadOnlyCollection<int> CompletingOrdersNumbers => _completingOrdersNumbers;

        public IReadOnlyCollection<int> CompletedOrdersNumbers => _completedOrdersNumbers;

        public void OnOrderAccepted(object sender, OrderAcceptedEventArgs e)
        {
            lock (_locker)
            {
                _completingOrdersNumbers.AddLast(e.Order.Number);
            }
        }

        public void OnCompletedOrderTaken(object sender, CompletedOrderTakenEventArgs e)
        {
            lock (_locker)
            {
                _completingOrdersNumbers.Remove(e.CompletedOrder.Number);
                _completedOrdersNumbers.Remove(e.CompletedOrder.Number);
            }
        }

        public void OnOrderCompleted(object sender, OrderCompletedEventArgs e)
        {
            lock (_locker)
            {
                _completingOrdersNumbers.Remove(e.CompletedOrder.Number);

                _completedOrdersNumbers.AddLast(e.CompletedOrder.Number);
            }

            OrderMarkedCompleted(this, new OrderMarkedCompletedEventArgs(e.CompletedOrder.Number));
        }
    }
}
