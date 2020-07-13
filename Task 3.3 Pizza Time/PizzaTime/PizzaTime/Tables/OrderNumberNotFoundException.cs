using System;

namespace PizzaTime.Tables
{
    [Serializable]
    public class OrderNumberNotFoundException : Exception
    {
        public OrderNumberNotFoundException(int orderNumber) => OrderNumber = orderNumber;
        public OrderNumberNotFoundException(int orderNumber, string message) : base(message) => OrderNumber = orderNumber;
        public OrderNumberNotFoundException(int orderNumber, string message, Exception inner) : base(message, inner) => OrderNumber = orderNumber;
        protected OrderNumberNotFoundException(
          int orderNumber,
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) => OrderNumber = orderNumber;

        public int OrderNumber { get; }
    }
}
