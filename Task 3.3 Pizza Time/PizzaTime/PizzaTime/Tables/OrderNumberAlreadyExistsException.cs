using System;

namespace PizzaTime.Tables
{
    [Serializable]
    public class OrderNumberAlreadyExistsException : Exception
    {
        public OrderNumberAlreadyExistsException(int orderNumber) => OrderNumber = orderNumber;
        public OrderNumberAlreadyExistsException(int orderNumber, string message) : base(message) => OrderNumber = orderNumber;
        public OrderNumberAlreadyExistsException(int orderNumber, string message, Exception inner) : base(message, inner) => OrderNumber = orderNumber;
        protected OrderNumberAlreadyExistsException(
          int orderNumber,
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) => OrderNumber = orderNumber;

        public int OrderNumber { get; }
    }
}
