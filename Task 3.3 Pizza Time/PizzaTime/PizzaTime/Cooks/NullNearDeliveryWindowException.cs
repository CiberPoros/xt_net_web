using System;
using System.Collections.Generic;
using System.Text;

namespace PizzaTime.Cooks
{

    [Serializable]
    public class NullNearDeliveryWindowException : NullReferenceException
    {
        public NullNearDeliveryWindowException() { }
        public NullNearDeliveryWindowException(string message) : base(message) { }
        public NullNearDeliveryWindowException(string message, Exception inner) : base(message, inner) { }
        protected NullNearDeliveryWindowException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
