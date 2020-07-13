using System;

namespace Restaurant
{

    [Serializable]
    public class ProductDeliveryWindowNotFoundException : Exception
    {
        public ProductDeliveryWindowNotFoundException() { }
        public ProductDeliveryWindowNotFoundException(string message) : base(message) { }
        public ProductDeliveryWindowNotFoundException(string message, Exception inner) : base(message, inner) { }
        protected ProductDeliveryWindowNotFoundException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
