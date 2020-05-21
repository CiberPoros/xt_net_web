using System;
using System.Collections.Generic;
using System.Linq;

namespace Task7
{
    internal static class HeaviestElementUtil
    {
        /// <summary>
        /// Определит наиболее "тяжелый" элемент коллекции, используя заданный компаратор
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <param name="comparison"></param>
        /// <returns></returns>
        internal static T GetHeaviestElement<T>(ICollection<T> collection, Comparison<T> comparison)
        {
            if (collection.Count == 0)
                throw new ArgumentException(nameof(collection), $"Collection { nameof(collection) } must have at least one element");

            T res = collection.First();
            foreach (var element in collection)
                if (comparison(element, res) == 1)
                    res = element;

            return res;
        }
    }
}
