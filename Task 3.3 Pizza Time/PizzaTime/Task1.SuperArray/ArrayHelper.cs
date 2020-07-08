using System;
using System.Collections.Generic;
using System.Linq;

namespace Task1.SuperArray
{
    public static class ArrayHelper
    {
        public static void ForEach<T>(this T[] array, Action<T> action)
        {
            if (array == null)
                throw new ArgumentNullException(nameof(array), $"Parameter {array} can't be null.");

            if (action == null)
                throw new ArgumentNullException(nameof(action), $"Parameter {action} can't be null.");

            foreach (var item in array)
                action(item);
        }

        public static void Transform<T>(this T[] array, Func<T, T> transformer)
        {
            if (array == null)
                throw new ArgumentNullException(nameof(array), $"Parameter {array} can't be null.");

            if (transformer == null)
                throw new ArgumentNullException(nameof(transformer), $"Parameter {transformer} can't be null.");

            for (int i = 0; i < array.Length; i++)
                array[i] = transformer(array[i]);
        }

        #region SUM

        #region NOT_NULLABLE
        public static int Sum(this int[] array)
        {
            if (array == null)
                throw new ArgumentNullException(nameof(array), $"Parameter {array} can't be null.");
            
            try
            {
                return array.Sum();
            }
            catch (OverflowException e)
            {
                throw new OverflowException("Ariphmetic operation resulted in an overflow.", e);
            }
        }

        public static long Sum(this long[] array)
        {
            if (array == null)
                throw new ArgumentNullException(nameof(array), $"Parameter {array} can't be null.");

            try
            {
                return array.Sum();
            }
            catch (OverflowException e)
            {
                throw new OverflowException("Ariphmetic operation resulted in an overflow.", e);
            }
        }

        public static double Sum(this double[] array)
        {
            if (array == null)
                throw new ArgumentNullException(nameof(array), $"Parameter {array} can't be null.");

            try
            {
                return array.Sum();
            }
            catch (OverflowException e)
            {
                throw new OverflowException("Ariphmetic operation resulted in an overflow.", e);
            }
        }

        public static decimal Sum(this decimal[] array)
        {
            if (array == null)
                throw new ArgumentNullException(nameof(array), $"Parameter {array} can't be null.");

            try
            {
                return array.Sum();
            }
            catch (OverflowException e)
            {
                throw new OverflowException("Ariphmetic operation resulted in an overflow.", e);
            }
        }

        public static float Sum(this float[] array)
        {
            if (array == null)
                throw new ArgumentNullException(nameof(array), $"Parameter {array} can't be null.");

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
        public static int? Sum(this int?[] array)
        {
            if (array == null)
                throw new ArgumentNullException(nameof(array), $"Parameter {array} can't be null.");

            try
            {
                return array.Sum();
            }
            catch (OverflowException e)
            {
                throw new OverflowException("Ariphmetic operation resulted in an overflow.", e);
            }
        }

        public static long? Sum(this long?[] array)
        {
            if (array == null)
                throw new ArgumentNullException(nameof(array), $"Parameter {array} can't be null.");

            try
            {
                return array.Sum();
            }
            catch (OverflowException e)
            {
                throw new OverflowException("Ariphmetic operation resulted in an overflow.", e);
            }
        }

        public static double? Sum(this double?[] array)
        {
            if (array == null)
                throw new ArgumentNullException(nameof(array), $"Parameter {array} can't be null.");

            try
            {
                return array.Sum();
            }
            catch (OverflowException e)
            {
                throw new OverflowException("Ariphmetic operation resulted in an overflow.", e);
            }
        }

        public static decimal? Sum(this decimal?[] array)
        {
            if (array == null)
                throw new ArgumentNullException(nameof(array), $"Parameter {array} can't be null.");

            try
            {
                return array.Sum();
            }
            catch (OverflowException e)
            {
                throw new OverflowException("Ariphmetic operation resulted in an overflow.", e);
            }
        }

        public static float? Sum(this float?[] array)
        {
            if (array == null)
                throw new ArgumentNullException(nameof(array), $"Parameter {array} can't be null.");

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
        public static int Sum<T>(this T[] array, Func<T, int> selector)
        {
            if (array == null)
                throw new ArgumentNullException(nameof(array), $"Parameter {array} can't be null.");

            if (selector == null)
                throw new ArgumentNullException(nameof(selector), $"Parameter {selector} can't be null.");

            try
            {
                return array.Sum(selector);
            }
            catch (OverflowException e)
            {
                throw new OverflowException("Ariphmetic operation resulted in an overflow.", e);
            }
        }

        public static long Sum<T>(this T[] array, Func<T, long> selector)
        {
            if (array == null)
                throw new ArgumentNullException(nameof(array), $"Parameter {array} can't be null.");

            if (selector == null)
                throw new ArgumentNullException(nameof(selector), $"Parameter {selector} can't be null.");

            try
            {
                return array.Sum(selector);
            }
            catch (OverflowException e)
            {
                throw new OverflowException("Ariphmetic operation resulted in an overflow.", e);
            }
        }

        public static double Sum<T>(this T[] array, Func<T, double> selector)
        {
            if (array == null)
                throw new ArgumentNullException(nameof(array), $"Parameter {array} can't be null.");

            if (selector == null)
                throw new ArgumentNullException(nameof(selector), $"Parameter {selector} can't be null.");

            try
            {
                return array.Sum(selector);
            }
            catch (OverflowException e)
            {
                throw new OverflowException("Ariphmetic operation resulted in an overflow.", e);
            }
        }

        public static decimal Sum<T>(this T[] array, Func<T, decimal> selector)
        {
            if (array == null)
                throw new ArgumentNullException(nameof(array), $"Parameter {array} can't be null.");

            if (selector == null)
                throw new ArgumentNullException(nameof(selector), $"Parameter {selector} can't be null.");

            try
            {
                return array.Sum(selector);
            }
            catch (OverflowException e)
            {
                throw new OverflowException("Ariphmetic operation resulted in an overflow.", e);
            }
        }

        public static float Sum<T>(this T[] array, Func<T, float> selector)
        {
            if (array == null)
                throw new ArgumentNullException(nameof(array), $"Parameter {array} can't be null.");

            if (selector == null)
                throw new ArgumentNullException(nameof(selector), $"Parameter {selector} can't be null.");

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
        public static int? Sum<T>(this T[] array, Func<T, int?> selector)
        {
            if (array == null)
                throw new ArgumentNullException(nameof(array), $"Parameter {array} can't be null.");

            if (selector == null)
                throw new ArgumentNullException(nameof(selector), $"Parameter {selector} can't be null.");

            try
            {
                return array.Sum(selector);
            }
            catch (OverflowException e)
            {
                throw new OverflowException("Ariphmetic operation resulted in an overflow.", e);
            }
        }

        public static long? Sum<T>(this T[] array, Func<T, long?> selector)
        {
            if (array == null)
                throw new ArgumentNullException(nameof(array), $"Parameter {array} can't be null.");

            if (selector == null)
                throw new ArgumentNullException(nameof(selector), $"Parameter {selector} can't be null.");

            try
            {
                return array.Sum(selector);
            }
            catch (OverflowException e)
            {
                throw new OverflowException("Ariphmetic operation resulted in an overflow.", e);
            }
        }

        public static double? Sum<T>(this T[] array, Func<T, double?> selector)
        {
            if (array == null)
                throw new ArgumentNullException(nameof(array), $"Parameter {array} can't be null.");

            if (selector == null)
                throw new ArgumentNullException(nameof(selector), $"Parameter {selector} can't be null.");

            try
            {
                return array.Sum(selector);
            }
            catch (OverflowException e)
            {
                throw new OverflowException("Ariphmetic operation resulted in an overflow.", e);
            }
        }

        public static decimal? Sum<T>(this T[] array, Func<T, decimal?> selector)
        {
            if (array == null)
                throw new ArgumentNullException(nameof(array), $"Parameter {array} can't be null.");

            if (selector == null)
                throw new ArgumentNullException(nameof(selector), $"Parameter {selector} can't be null.");

            try
            {
                return array.Sum(selector);
            }
            catch (OverflowException e)
            {
                throw new OverflowException("Ariphmetic operation resulted in an overflow.", e);
            }
        }

        public static float? Sum<T>(this T[] array, Func<T, float?> selector)
        {
            if (array == null)
                throw new ArgumentNullException(nameof(array), $"Parameter {array} can't be null.");

            if (selector == null)
                throw new ArgumentNullException(nameof(selector), $"Parameter {selector} can't be null.");

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
    }
}
