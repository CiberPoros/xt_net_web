using System;
using System.Linq;

namespace Common
{
    public static class ArraysUtils
    {
        /// <summary>
        /// Генерирует массив с заданным количеством измерений и заполняет его елементами, получаемыми с помощью заданного генератора
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="rank"></param>
        /// <param name="degreesLengths"></param>
        /// <param name="generator"></param>
        /// <returns></returns>
        public static Array GenerateArray<T>(int[] degreesLengths, Func<T> generator)
        {
            if (degreesLengths.Length == 0)
                throw new ArgumentException($"{ nameof(degreesLengths) } must have at least one element", nameof(degreesLengths));
            if (degreesLengths.Any(val => val <= 0))
                throw new ArgumentException($"{ nameof(degreesLengths) } must contains only positive elements", nameof(degreesLengths));

            int rank = degreesLengths.Length;
            var array = Array.CreateInstance(typeof(T), degreesLengths);

            int[] currentIndexes = new int[rank];
            for (; ; )  // тут мб и стоило сделать рекурсивно, т.к. итеративный подход тут менее очевиден
            {           // а глубина рекурсии всё равно была бы не больше количества измерений массива...
                array.SetValue(generator(), currentIndexes);

                for (int k = rank - 1; ; k--)
                {
                    if (k < 0)
                        return array;

                    if (currentIndexes[k] < degreesLengths[k] - 1)
                    {
                        currentIndexes[k]++;
                        break;
                    }

                    currentIndexes[k] = 0;
                }
            }
        }

        /// <summary>
        /// Заменяет все элементы массива с произвольным количествои измерении в соответствии с заданным предикатом на заданное значение
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array"></param>
        /// <param name="isRepacable"></param>
        /// <param name="newValue"></param>
        public static void ReplaceElements<T>(Array array, Predicate<T> isRepacable, T newValue)
        {
            int rank = array.Rank;
            int[] degreesLengths = new int[rank];
            for (int i = 0; i < rank; i++)
                degreesLengths[i] = array.GetLength(i); // кэширую, т.к. не ясно, сколько по времени эта штука работает

            int[] currentIndexes = new int[rank];
            for (; ; )
            {
                if (isRepacable((T)array.GetValue(currentIndexes)))
                    array.SetValue(newValue, currentIndexes);

                for (int k = rank - 1; ; k--)
                {
                    if (k < 0)
                        return;

                    if (currentIndexes[k] < degreesLengths[k] - 1)
                    {
                        currentIndexes[k]++;
                        break;
                    }

                    currentIndexes[k] = 0;
                }
            }
        }
    }
}
