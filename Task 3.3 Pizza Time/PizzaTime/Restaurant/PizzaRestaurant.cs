using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using PizzaTime.Cashiers;
using PizzaTime.Clients;
using PizzaTime.Cooks;
using PizzaTime.OrdersControllers;
using PizzaTime.ProductDeliveryWindows;
using PizzaTime.Tables;

namespace Restaurant
{
    public class PizzaRestaurant
    {
        private static readonly Random _random = new Random();

        private readonly List<ICashier> _cashiers;
        private readonly List<ICook> _cooks;
        private readonly List<ITable> _tables;

        public PizzaRestaurant()
        {
            _cashiers = new List<ICashier>();
            _cooks = new List<ICook>();
            _tables = new List<ITable>();
        }

        // вариант, что у кассира очередь клиентов, не рассматривается, заказ принимается моментально
        public ICashier GetNearCashier() => 
            _cashiers.Any() ? _cashiers[_random.Next(_cashiers.Count)] : throw new Exception("Restaurant doesn't have at least one cashier.");

        // у ресторана много панелей, где высвечиваются заказы, поэтому пусть клиент подписывается просто на ближайшее табло
        public ITable GetNearTable() =>
            _tables.Any()? _tables[_random.Next(_tables.Count)] : throw new Exception("Restaurant doesn't have at least one table.");

        public void AddCashier(ICashier cashier)
        {
            if (cashier == null)
                throw new ArgumentNullException(nameof(cashier), "Parameter is null.");

            if (_cashiers.Contains(cashier))
                throw new ArgumentException("Cashier already exists.", nameof(cashier));

            _cashiers.Add(cashier);
        }

        public bool RemoveCashier(ICashier cashier)
        {
            if (cashier == null)
                throw new ArgumentNullException(nameof(cashier), "Parameter is null.");

            return _cashiers.Remove(cashier);
        }

        public void AddCook(ICook cook) 
        {
            if (cook == null)
                throw new ArgumentNullException(nameof(cook), "Parameter is null.");

            if (_cooks.Contains(cook))
                throw new ArgumentException("Cook already exists.", nameof(cook));

            OrderController.Instance.OrderAdded += cook.OnOrderAdded;

            _cooks.Add(cook);
        }

        public bool RemoveCook(ICook cook)
        {
            if (cook == null)
                throw new ArgumentNullException(nameof(cook), "Parameter is null.");

            if (!_cooks.Contains(cook))
                return false;

            OrderController.Instance.OrderAdded -= cook.OnOrderAdded;

            return _cooks.Remove(cook);
        }

        public void AddTable(ITable table)
        {
            if (table == null)
                throw new ArgumentNullException(nameof(table), "Parameter is null.");

            if (_tables.Contains(table))
                throw new ArgumentException("Table already exists.", nameof(table));

            foreach (var cashier in _cashiers)
                cashier.OrderAccepted += table.OnOrderAccepted;

            foreach (var cook in _cooks)
                cook.OrderCompleted += table.OnOrderCompleted;

            ProductDeliveryWindow.Instance.CompletedOrderTaken += table.OnCompletedOrderTaken;

            _tables.Add(table);
        }

        public bool RemoveTable(ITable table)
        {
            if (table == null)
                throw new ArgumentNullException(nameof(table), "Parameter is null.");

            if (!_tables.Contains(table))
                return false;

            foreach (var cashier in _cashiers)
                cashier.OrderAccepted -= table.OnOrderAccepted;

            foreach (var cook in _cooks)
                cook.OrderCompleted -= table.OnOrderCompleted;

            ProductDeliveryWindow.Instance.CompletedOrderTaken -= table.OnCompletedOrderTaken;

            return _tables.Remove(table);
        }
    }
}
