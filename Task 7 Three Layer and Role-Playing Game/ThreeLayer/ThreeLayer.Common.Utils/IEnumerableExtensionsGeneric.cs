using System;
using System.Collections.Generic;
using System.Linq;

namespace ThreeLayer.Common.Utils
{
    public static class IEnumerableExtensionsGeneric
    {
        public static T MaxOrDefault<T>(this IEnumerable<T> elements) => elements.Any() ? elements.Max() : default;
        public static T MaxOrDefault<T>(this IEnumerable<T> elements, T defaultValue) => elements.Any() ? elements.Max() : defaultValue;
        public static TResult MaxOrDefault<TSource, TResult>(this IEnumerable<TSource> elements, Func<TSource, TResult> selector) =>
             elements.Any() ? elements.Max(selector) : default;
        public static TResult MaxOrDefault<TSource, TResult>(this IEnumerable<TSource> elements, TResult defaultValue, Func<TSource, TResult> selector) =>
             elements.Any() ? elements.Max(selector) : defaultValue;

        public static T MinOrDefault<T>(this IEnumerable<T> elements) => elements.Any() ? elements.Min() : default;
        public static T MinOrDefault<T>(this IEnumerable<T> elements, T defaultValue) => elements.Any() ? elements.Min() : defaultValue;
        public static TResult MinOrDefault<TSource, TResult>(this IEnumerable<TSource> elements, Func<TSource, TResult> selector) =>
             elements.Any() ? elements.Min(selector) : default;
        public static TResult MinOrDefault<TSource, TResult>(this IEnumerable<TSource> elements, TResult defaultValue, Func<TSource, TResult> selector) =>
             elements.Any() ? elements.Min(selector) : defaultValue;
    }
}
