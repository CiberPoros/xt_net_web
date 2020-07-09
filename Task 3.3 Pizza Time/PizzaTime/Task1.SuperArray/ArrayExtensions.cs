using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography;

namespace Task1.SuperArray
{
    public static class ArrayExtensions
    {
        public static void ForEach<T>(this T[] array, Action<T> action)
        {
            if (array == null)
                throw new ArgumentNullException(nameof(array), $"Parameter {nameof(array)} can't be null.");

            if (action == null)
                throw new ArgumentNullException(nameof(action), $"Parameter {nameof(action)} can't be null.");

            foreach (var item in array)
                action(item);
        }

        public static void Transform<T>(this T[] array, Func<T, T> transformer)
        {
            if (array == null)
                throw new ArgumentNullException(nameof(array), $"Parameter {nameof(array)} can't be null.");

            if (transformer == null)
                throw new ArgumentNullException(nameof(transformer), $"Parameter {nameof(transformer)} can't be null.");

            for (int i = 0; i < array.Length; i++)
                array[i] = transformer(array[i]);
        }

        public static T GetCommonest<T>(this T[] array)
        {
            if (array == null)
                throw new ArgumentNullException(nameof(array), $"Parameter {nameof(array)} can't be null.");

            if (!array.Any())
                throw new ArgumentException($"Parameter {nameof(array)} must contain at least one element.", nameof(array));

            return array
                .GroupBy(val => val)
                .Aggregate((commonest, next) => next.Count() > commonest.Count() ? next : commonest)
                .Key;
        }

        #region SUM

        #region NOT_NULLABLE
        public static int GetSum(this int[] array)
        {
            if (array == null)
                throw new ArgumentNullException(nameof(array), $"Parameter {nameof(array)} can't be null.");
            
            try
            {
                return array.Sum();
            }
            catch (OverflowException e)
            {
                throw new OverflowException("Ariphmetic operation resulted in an overflow.", e);
            }
        }

        public static long GetSum(this long[] array)
        {
            if (array == null)
                throw new ArgumentNullException(nameof(array), $"Parameter {nameof(array)} can't be null.");

            try
            {
                return array.Sum();
            }
            catch (OverflowException e)
            {
                throw new OverflowException("Ariphmetic operation resulted in an overflow.", e);
            }
        }

        public static double GetSum(this double[] array)
        {
            if (array == null)
                throw new ArgumentNullException(nameof(array), $"Parameter {nameof(array)} can't be null.");

            try
            {
                return array.Sum();
            }
            catch (OverflowException e)
            {
                throw new OverflowException("Ariphmetic operation resulted in an overflow.", e);
            }
        }

        public static decimal GetSum(this decimal[] array)
        {
            if (array == null)
                throw new ArgumentNullException(nameof(array), $"Parameter {nameof(array)} can't be null.");

            try
            {
                return array.Sum();
            }
            catch (OverflowException e)
            {
                throw new OverflowException("Ariphmetic operation resulted in an overflow.", e);
            }
        }

        public static float GetSum(this float[] array)
        {
            if (array == null)
                throw new ArgumentNullException(nameof(array), $"Parameter {nameof(array)} can't be null.");

            try
            {
                return array.Sum();
            }
            catch (OverflowException e)
            {
                throw new OverflowException("Ariphmetic operation resulted in an overflow.", e);
            }
        }
        #endregion

        #region NULLABLE
        public static int? GetSum(this int?[] array)
        {
            if (array == null)
                throw new ArgumentNullException(nameof(array), $"Parameter {nameof(array)} can't be null.");

            try
            {
                return array.Sum();
            }
            catch (OverflowException e)
            {
                throw new OverflowException("Ariphmetic operation resulted in an overflow.", e);
            }
        }

        public static long? GetSum(this long?[] array)
        {
            if (array == null)
                throw new ArgumentNullException(nameof(array), $"Parameter {nameof(array)} can't be null.");

            try
            {
                return array.Sum();
            }
            catch (OverflowException e)
            {
                throw new OverflowException("Ariphmetic operation resulted in an overflow.", e);
            }
        }

        public static double? GetSum(this double?[] array)
        {
            if (array == null)
                throw new ArgumentNullException(nameof(array), $"Parameter {nameof(array)} can't be null.");

            try
            {
                return array.Sum();
            }
            catch (OverflowException e)
            {
                throw new OverflowException("Ariphmetic operation resulted in an overflow.", e);
            }
        }

        public static decimal? GetSum(this decimal?[] array)
        {
            if (array == null)
                throw new ArgumentNullException(nameof(array), $"Parameter {nameof(array)} can't be null.");

            try
            {
                return array.Sum();
            }
            catch (OverflowException e)
            {
                throw new OverflowException("Ariphmetic operation resulted in an overflow.", e);
            }
        }

        public static float? GetSum(this float?[] array)
        {
            if (array == null)
                throw new ArgumentNullException(nameof(array), $"Parameter {nameof(array)} can't be null.");

            try
            {
                return array.Sum();
            }
            catch (OverflowException e)
            {
                throw new OverflowException("Ariphmetic operation resulted in an overflow.", e);
            }
        }
        #endregion

        #region GENERIC_WITH_SELECTOR_TO_NOT_NULLABLE
        public static int GetSum<T>(this T[] array, Func<T, int> selector)
        {
            if (array == null)
                throw new ArgumentNullException(nameof(array), $"Parameter {nameof(array)} can't be null.");

            if (selector == null)
                throw new ArgumentNullException(nameof(selector), $"Parameter {nameof(selector)} can't be null.");

            try
            {
                return array.Sum(selector);
            }
            catch (OverflowException e)
            {
                throw new OverflowException("Ariphmetic operation resulted in an overflow.", e);
            }
        }

        public static long GetSum<T>(this T[] array, Func<T, long> selector)
        {
            if (array == null)
                throw new ArgumentNullException(nameof(array), $"Parameter {nameof(array)} can't be null.");

            if (selector == null)
                throw new ArgumentNullException(nameof(selector), $"Parameter {nameof(selector)} can't be null.");

            try
            {
                return array.Sum(selector);
            }
            catch (OverflowException e)
            {
                throw new OverflowException("Ariphmetic operation resulted in an overflow.", e);
            }
        }

        public static double GetSum<T>(this T[] array, Func<T, double> selector)
        {
            if (array == null)
                throw new ArgumentNullException(nameof(array), $"Parameter {nameof(array)} can't be null.");

            if (selector == null)
                throw new ArgumentNullException(nameof(selector), $"Parameter {nameof(selector)} can't be null.");

            try
            {
                return array.Sum(selector);
            }
            catch (OverflowException e)
            {
                throw new OverflowException("Ariphmetic operation resulted in an overflow.", e);
            }
        }

        public static decimal GetSum<T>(this T[] array, Func<T, decimal> selector)
        {
            if (array == null)
                throw new ArgumentNullException(nameof(array), $"Parameter {nameof(array)} can't be null.");

            if (selector == null)
                throw new ArgumentNullException(nameof(selector), $"Parameter {nameof(selector)} can't be null.");

            try
            {
                return array.Sum(selector);
            }
            catch (OverflowException e)
            {
                throw new OverflowException("Ariphmetic operation resulted in an overflow.", e);
            }
        }

        public static float GetSum<T>(this T[] array, Func<T, float> selector)
        {
            if (array == null)
                throw new ArgumentNullException(nameof(array), $"Parameter {nameof(array)} can't be null.");

            if (selector == null)
                throw new ArgumentNullException(nameof(selector), $"Parameter {nameof(selector)} can't be null.");

            try
            {
                return array.Sum(selector);
            }
            catch (OverflowException e)
            {
                throw new OverflowException("Ariphmetic operation resulted in an overflow.", e);
            }
        }
        #endregion

        #region GENERIC_WITH_SELECTOR_TO_NULLABLE
        public static int? GetSum<T>(this T[] array, Func<T, int?> selector)
        {
            if (array == null)
                throw new ArgumentNullException(nameof(array), $"Parameter {nameof(array)} can't be null.");

            if (selector == null)
                throw new ArgumentNullException(nameof(selector), $"Parameter {nameof(selector)} can't be null.");

            try
            {
                return array.Sum(selector);
            }
            catch (OverflowException e)
            {
                throw new OverflowException("Ariphmetic operation resulted in an overflow.", e);
            }
        }

        public static long? GetSum<T>(this T[] array, Func<T, long?> selector)
        {
            if (array == null)
                throw new ArgumentNullException(nameof(array), $"Parameter {nameof(array)} can't be null.");

            if (selector == null)
                throw new ArgumentNullException(nameof(selector), $"Parameter {nameof(selector)} can't be null.");

            try
            {
                return array.Sum(selector);
            }
            catch (OverflowException e)
            {
                throw new OverflowException("Ariphmetic operation resulted in an overflow.", e);
            }
        }

        public static double? GetSum<T>(this T[] array, Func<T, double?> selector)
        {
            if (array == null)
                throw new ArgumentNullException(nameof(array), $"Parameter {nameof(array)} can't be null.");

            if (selector == null)
                throw new ArgumentNullException(nameof(selector), $"Parameter {nameof(selector)} can't be null.");

            try
            {
                return array.Sum(selector);
            }
            catch (OverflowException e)
            {
                throw new OverflowException("Ariphmetic operation resulted in an overflow.", e);
            }
        }

        public static decimal? GetSum<T>(this T[] array, Func<T, decimal?> selector)
        {
            if (array == null)
                throw new ArgumentNullException(nameof(array), $"Parameter {nameof(array)} can't be null.");

            if (selector == null)
                throw new ArgumentNullException(nameof(selector), $"Parameter {nameof(selector)} can't be null.");

            try
            {
                return array.Sum(selector);
            }
            catch (OverflowException e)
            {
                throw new OverflowException("Ariphmetic operation resulted in an overflow.", e);
            }
        }

        public static float? GetSum<T>(this T[] array, Func<T, float?> selector)
        {
            if (array == null)
                throw new ArgumentNullException(nameof(array), $"Parameter {nameof(array)} can't be null.");

            if (selector == null)
                throw new ArgumentNullException(nameof(selector), $"Parameter {nameof(selector)} can't be null.");

            try
            {
                return array.Sum(selector);
            }
            catch (OverflowException e)
            {
                throw new OverflowException("Ariphmetic operation resulted in an overflow.", e);
            }
        }
        #endregion

        #endregion

        #region AVERAGE

        #region NOT_NULLABLE

        public static double GetAverage(this int[] array)
        {
            if (array == null)
                throw new ArgumentNullException(nameof(array), $"Parameter {nameof(array)} can't be null.");

            if (!array.Any())
                throw new ArgumentException($"Parameter {nameof(array)} must contain at least one element.", nameof(array));

            return array.Average();
        }

        public static double GetAverage(this long[] array)
        {
            if (array == null)
                throw new ArgumentNullException(nameof(array), $"Parameter {nameof(array)} can't be null.");

            if (!array.Any())
                throw new ArgumentException($"Parameter {nameof(array)} must contain at least one element.", nameof(array));

            return array.Average();
        }

        public static double GetAverage(this double[] array)
        {
            if (array == null)
                throw new ArgumentNullException(nameof(array), $"Parameter {nameof(array)} can't be null.");

            if (!array.Any())
                throw new ArgumentException($"Parameter {nameof(array)} must contain at least one element.", nameof(array));

            return array.Average();
        }

        public static decimal GetAverage(this decimal[] array)
        {
            if (array == null)
                throw new ArgumentNullException(nameof(array), $"Parameter {nameof(array)} can't be null.");

            if (!array.Any())
                throw new ArgumentException($"Parameter {nameof(array)} must contain at least one element.", nameof(array));

            return array.Average();
        }

        public static float GetAverage(this float[] array)
        {
            if (array == null)
                throw new ArgumentNullException(nameof(array), $"Parameter {nameof(array)} can't be null.");

            if (!array.Any())
                throw new ArgumentException($"Parameter {nameof(array)} must contain at least one element.", nameof(array));

            return array.Average();
        }

        #endregion

        #region NULLABLE

        public static double? GetAverage(this int?[] array)
        {
            if (array == null)
                throw new ArgumentNullException(nameof(array), $"Parameter {nameof(array)} can't be null.");

            if (!array.Any())
                throw new ArgumentException($"Parameter {nameof(array)} must contain at least one element.", nameof(array));

            return array.Average();
        }

        public static double? GetAverage(this long?[] array)
        {
            if (array == null)
                throw new ArgumentNullException(nameof(array), $"Parameter {nameof(array)} can't be null.");

            if (!array.Any())
                throw new ArgumentException($"Parameter {nameof(array)} must contain at least one element.", nameof(array));

            return array.Average();
        }

        public static double? GetAverage(this double?[] array)
        {
            if (array == null)
                throw new ArgumentNullException(nameof(array), $"Parameter {nameof(array)} can't be null.");

            if (!array.Any())
                throw new ArgumentException($"Parameter {nameof(array)} must contain at least one element.", nameof(array));

            return array.Average();
        }

        public static decimal? GetAverage(this decimal?[] array)
        {
            if (array == null)
                throw new ArgumentNullException(nameof(array), $"Parameter {nameof(array)} can't be null.");

            if (!array.Any())
                throw new ArgumentException($"Parameter {nameof(array)} must contain at least one element.", nameof(array));

            return array.Average();
        }

        public static float? GetAverage(this float?[] array)
        {
            if (array == null)
                throw new ArgumentNullException(nameof(array), $"Parameter {nameof(array)} can't be null.");

            if (!array.Any())
                throw new ArgumentException($"Parameter {nameof(array)} must contain at least one element.", nameof(array));

            return array.Average();
        }

        #endregion

        #region GENERIC_WITH_SELECTOR_TO_NOT_NULLABLE

        public static double GetAverage<T>(this T[] array, Func<T, int> selector)
        {
            if (array == null)
                throw new ArgumentNullException(nameof(array), $"Parameter {nameof(array)} can't be null.");

            if (selector == null)
                throw new ArgumentNullException(nameof(selector), $"Parameter {nameof(selector)} can't be null.");

            if (!array.Any())
                throw new ArgumentException($"Parameter {nameof(array)} must contain at least one element.", nameof(array));

            return array.Average(selector);
        }

        public static double GetAverage<T>(this T[] array, Func<T, long> selector)
        {
            if (array == null)
                throw new ArgumentNullException(nameof(array), $"Parameter {nameof(array)} can't be null.");

            if (selector == null)
                throw new ArgumentNullException(nameof(selector), $"Parameter {nameof(selector)} can't be null.");

            if (!array.Any())
                throw new ArgumentException($"Parameter {nameof(array)} must contain at least one element.", nameof(array));

            return array.Average(selector);
        }

        public static double GetAverage<T>(this T[] array, Func<T, double> selector)
        {
            if (array == null)
                throw new ArgumentNullException(nameof(array), $"Parameter {nameof(array)} can't be null.");

            if (selector == null)
                throw new ArgumentNullException(nameof(selector), $"Parameter {nameof(selector)} can't be null.");

            if (!array.Any())
                throw new ArgumentException($"Parameter {nameof(array)} must contain at least one element.", nameof(array));

            return array.Average(selector);
        }

        public static decimal GetAverage<T>(this T[] array, Func<T, decimal> selector)
        {
            if (array == null)
                throw new ArgumentNullException(nameof(array), $"Parameter {nameof(array)} can't be null.");

            if (selector == null)
                throw new ArgumentNullException(nameof(selector), $"Parameter {nameof(selector)} can't be null.");

            if (!array.Any())
                throw new ArgumentException($"Parameter {nameof(array)} must contain at least one element.", nameof(array));

            return array.Average(selector);
        }

        public static float GetAverage<T>(this T[] array, Func<T, float> selector)
        {
            if (array == null)
                throw new ArgumentNullException(nameof(array), $"Parameter {nameof(array)} can't be null.");

            if (selector == null)
                throw new ArgumentNullException(nameof(selector), $"Parameter {nameof(selector)} can't be null.");

            if (!array.Any())
                throw new ArgumentException($"Parameter {nameof(array)} must contain at least one element.", nameof(array));

            return array.Average(selector);
        }

        #endregion

        #region GENERIC_WITH_SELECTOR_TO_NULLABLE

        public static double? GetAverage<T>(this T[] array, Func<T, int?> selector)
        {
            if (array == null)
                throw new ArgumentNullException(nameof(array), $"Parameter {nameof(array)} can't be null.");

            if (selector == null)
                throw new ArgumentNullException(nameof(selector), $"Parameter {nameof(selector)} can't be null.");

            if (!array.Any())
                throw new ArgumentException($"Parameter {nameof(array)} must contain at least one element.", nameof(array));

            return array.Average(selector);
        }

        public static double? GetAverage<T>(this T[] array, Func<T, long?> selector)
        {
            if (array == null)
                throw new ArgumentNullException(nameof(array), $"Parameter {nameof(array)} can't be null.");

            if (selector == null)
                throw new ArgumentNullException(nameof(selector), $"Parameter {nameof(selector)} can't be null.");

            if (!array.Any())
                throw new ArgumentException($"Parameter {nameof(array)} must contain at least one element.", nameof(array));

            return array.Average(selector);
        }

        public static double? GetAverage<T>(this T[] array, Func<T, double?> selector)
        {
            if (array == null)
                throw new ArgumentNullException(nameof(array), $"Parameter {nameof(array)} can't be null.");

            if (selector == null)
                throw new ArgumentNullException(nameof(selector), $"Parameter {nameof(selector)} can't be null.");

            if (!array.Any())
                throw new ArgumentException($"Parameter {nameof(array)} must contain at least one element.", nameof(array));

            return array.Average(selector);
        }

        public static decimal? GetAverage<T>(this T[] array, Func<T, decimal?> selector)
        {
            if (array == null)
                throw new ArgumentNullException(nameof(array), $"Parameter {nameof(array)} can't be null.");

            if (selector == null)
                throw new ArgumentNullException(nameof(selector), $"Parameter {nameof(selector)} can't be null.");

            if (!array.Any())
                throw new ArgumentException($"Parameter {nameof(array)} must contain at least one element.", nameof(array));

            return array.Average(selector);
        }

        public static float? GetAverage<T>(this T[] array, Func<T, float?> selector)
        {
            if (array == null)
                throw new ArgumentNullException(nameof(array), $"Parameter {nameof(array)} can't be null.");

            if (selector == null)
                throw new ArgumentNullException(nameof(selector), $"Parameter {nameof(selector)} can't be null.");

            if (!array.Any())
                throw new ArgumentException($"Parameter {nameof(array)} must contain at least one element.", nameof(array));

            return array.Average(selector);
        }

        #endregion

        #endregion
    }
}
