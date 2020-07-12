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
using PizzaTime.Restaurants;

namespace Restaurant
{
    public class PizzaRestaurant : IRestaurant
    {
        private static readonly Random _random = new Random();

        private readonly List<ICashier> _cashiers;
        private readonly List<ICook> _cooks;
        private readonly List<ITable> _tables;
        private readonly Dictionary<int, AbstractProductDeliveryWindow> _productDeliveryWindowsByNumber;

        private readonly IOrderController _orderController;

        public PizzaRestaurant()
        {
            _cashiers = new List<ICashier>();
            _cooks = new List<ICook>();
            _tables = new List<ITable>();
            _productDeliveryWindowsByNumber = new Dictionary<int, AbstractProductDeliveryWindow>();

            _orderController = new OrderController();
        }

        public IReadOnlyList<ICashier> Cashiers => _cashiers;
        public IReadOnlyList<ITable> Tables => _tables;
        public IReadOnlyDictionary<int, AbstractProductDeliveryWindow> ProductDeliveryWindows => _productDeliveryWindowsByNumber;

        public void AddProductDeliveryWindow(AbstractProductDeliveryWindow productDeliveryWindow)
        {
            if (productDeliveryWindow == null)
                throw new ArgumentNullException(nameof(productDeliveryWindow), "Parameter is null.");

            if (_productDeliveryWindowsByNumber.ContainsKey(productDeliveryWindow.WindowNumber))
                throw new ArgumentException("Product delivery window already exists.", nameof(productDeliveryWindow));

            _productDeliveryWindowsByNumber.Add(productDeliveryWindow.WindowNumber, productDeliveryWindow);
        }

        public bool RemoveProductDeliveryWindow(AbstractProductDeliveryWindow productDeliveryWindow)
        {
            if (productDeliveryWindow == null)
                throw new ArgumentNullException(nameof(productDeliveryWindow), "Parameter is null.");

            return _productDeliveryWindowsByNumber.Remove(productDeliveryWindow.WindowNumber);
        }

        public void AddCashier(ICashier cashier)
        {
            if (cashier == null)
                throw new ArgumentNullException(nameof(cashier), "Parameter is null.");

            if (_cashiers.Contains(cashier))
                throw new ArgumentException("Cashier already exists.", nameof(cashier));

            cashier.OrderController = _orderController;
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

            if (!_productDeliveryWindowsByNumber.Any())
                throw new ProductDeliveryWindowNotFoundException("Product delivery window list is empty! The first add at least one element.");

            cook.OrderController = _orderController;

            int _nearProductDeliveryWindowNumber = _random.Next(_productDeliveryWindowsByNumber.Count); // будем считать, что это выбор ближайшего
            cook.NearProductDeliveryWindow = _productDeliveryWindowsByNumber.ElementAt(_nearProductDeliveryWindowNumber).Value;

            _cooks.Add(cook);
        }

        public bool RemoveCook(ICook cook)
        {
            if (cook == null)
                throw new ArgumentNullException(nameof(cook), "Parameter is null.");

            if (!_cooks.Contains(cook))
                return false;

            cook.NearProductDeliveryWindow = null;
            cook.OrderController = null;

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

            foreach (var productDeliverywindow in _productDeliveryWindowsByNumber)
                productDeliverywindow.Value.CompletedOrderTaken += table.OnCompletedOrderTaken;

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

            foreach (var productDeliverywindow in _productDeliveryWindowsByNumber)
                productDeliverywindow.Value.CompletedOrderTaken -= table.OnCompletedOrderTaken;

            return _tables.Remove(table);
        }
    }
}
