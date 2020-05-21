using System;
using System.Collections.Generic;

namespace Common
{
    public static class CollectionsGenerator
    {
        public static ICollection<T> GenerateCollection<T>(int length, Func<T> generator)
        {
            if (length < 0)
                throw new ArgumentOutOfRangeException(nameof(length), $"{ nameof(length) } must be positive");

            List<T> result = new List<T>(length);
            for (int i = 0; i < length; i++)
                result.Add(generator());

            return result;
        }
    }
}
