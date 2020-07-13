using System;
using System.Collections.Generic;
using PizzaTime.Cashiers;
using PizzaTime.Cooks;
using PizzaTime.OrdersControllers;
using PizzaTime.ProductDeliveryWindows;
using PizzaTime.Tables;

namespace PizzaTime.Restaurants
{
    public abstract class AbstractRestaurant : IRestaurantUI, IRestaurant
    {
        protected readonly List<AbstractCashier> _cashiers;
        protected readonly List<AbstractCook> _cooks;
        protected readonly List<AbstractTable> _tables;
        protected readonly Dictionary<int, AbstractProductDeliveryWindow> _productDeliveryWindowsByNumber;

        protected IOrdersController _ordersController;

        public AbstractRestaurant(IOrdersController ordersController)
        {
            _ordersController = ordersController ?? throw new ArgumentNullException(nameof(ordersController), "Argument is null.");

            _cashiers = new List<AbstractCashier>();
            _cooks = new List<AbstractCook>();
            _tables = new List<AbstractTable>();
            _productDeliveryWindowsByNumber = new Dictionary<int, AbstractProductDeliveryWindow>();

            CashierAdded = delegate { };
            CashierRemoved = delegate { };
            CookAdded = delegate { };
            CookRemoved = delegate { };
            TableAdded = delegate { };
            TableRemoved = delegate { };
            ProductDeliveryWindowAdded = delegate { };
            ProductDeliveryWindowRemoved = delegate { };
        }

        public event EventHandler<AbstractCashier> CashierAdded;
        public event EventHandler<AbstractCashier> CashierRemoved;

        public event EventHandler<AbstractCook> CookAdded;
        public event EventHandler<AbstractCook> CookRemoved;

        public event EventHandler<AbstractTable> TableAdded;
        public event EventHandler<AbstractTable> TableRemoved;

        public event EventHandler<AbstractProductDeliveryWindow> ProductDeliveryWindowAdded;
        public event EventHandler<AbstractProductDeliveryWindow> ProductDeliveryWindowRemoved;

        public IReadOnlyCollection<AbstractCashier> Cashiers => _cashiers;
        public IReadOnlyCollection<AbstractTable> Tables => _tables;
        public IReadOnlyCollection<AbstractCook> Cooks => _cooks;
        public IReadOnlyDictionary<int, AbstractProductDeliveryWindow> ProductDeliveryWindowsByNumber => _productDeliveryWindowsByNumber;

        public IOrdersController OrdersController => _ordersController;

        public IProductDeliveryWindow GetProductDeliveryWindowByNumber(int number)
        {
            if (!_productDeliveryWindowsByNumber.TryGetValue(number, out AbstractProductDeliveryWindow productDeliveryWindow))
                throw new KeyNotFoundException("Product delivery window with this number was not found.");

            return productDeliveryWindow;
        }

        public virtual void AddProductDeliveryWindow(AbstractProductDeliveryWindow productDeliveryWindow)
        {
            if (productDeliveryWindow == null)
                throw new ArgumentNullException(nameof(productDeliveryWindow));

            if (_productDeliveryWindowsByNumber.ContainsKey(productDeliveryWindow.WindowNumber))
                throw new ArgumentException("ProductDeliveryWindow already contained in productDeliveryWindow collection.", nameof(productDeliveryWindow));

            ProductDeliveryWindowAdded(this, productDeliveryWindow);

            _productDeliveryWindowsByNumber.Add(productDeliveryWindow.WindowNumber, productDeliveryWindow);
        }
        public virtual bool RemoveProductDeliveryWindow(AbstractProductDeliveryWindow productDeliveryWindow)
        {
            if (productDeliveryWindow == null)
                throw new ArgumentNullException(nameof(productDeliveryWindow));

            if (!_productDeliveryWindowsByNumber.Remove(productDeliveryWindow.WindowNumber))
                return false;

            ProductDeliveryWindowRemoved(this, productDeliveryWindow);

            return true;
        }

        public virtual void AddCashier(AbstractCashier cashier)
        {
            if (cashier == null)
                throw new ArgumentNullException(nameof(cashier));

            if (_cashiers.Contains(cashier))
                throw new ArgumentException("Cashier already contained in cashiers list.", nameof(cashier));

            _cashiers.Add(cashier);

            CashierAdded(this, cashier);
        }
        public virtual bool RemoveCashier(AbstractCashier cashier)
        {
            if (cashier == null)
                throw new ArgumentNullException(nameof(cashier));

            if (!_cashiers.Remove(cashier))
                return false;

            CashierRemoved(this, cashier);

            return true;
        }

        public virtual void AddCook(AbstractCook cook)
        {
            if (cook == null)
                throw new ArgumentNullException(nameof(cook));

            if (_cooks.Contains(cook))
                throw new ArgumentException("Cook already contained in cooks list.", nameof(cook));

            _cooks.Add(cook);

            CookAdded(this, cook);
        }
        public virtual bool RemoveCook(AbstractCook cook)
        {
            if (cook == null)
                throw new ArgumentNullException(nameof(cook));

            if (!_cooks.Remove(cook))
                return false;

            CookRemoved(this, cook);

            return true;
        }

        public virtual void AddTable(AbstractTable table)
        {
            if (table == null)
                throw new ArgumentNullException(nameof(table));

            if (_tables.Contains(table))
                throw new ArgumentException("Table already contained in tables list.", nameof(table));

            _tables.Add(table);

            TableAdded(this, table);
        }
        public virtual bool RemoveTable(AbstractTable table)
        {
            if (table == null)
                throw new ArgumentNullException(nameof(table));

            if (!_tables.Remove(table))
                return false;

            TableRemoved(this, table);

            return true;
        }
    }
}
