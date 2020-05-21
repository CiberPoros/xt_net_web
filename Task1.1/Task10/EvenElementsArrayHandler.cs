using System;
using System.Linq;
using System.Text;
using Common;

namespace Task10
{
    internal class EvenElementsArrayHandler : Handler
    {
        private readonly static Random _rnd;

        const int ValueDownLimit = -100;
        const int ValueUpLimit = 100;
        const int DefaultWidth = 10;
        const int DefaultHeight = 10;

        static EvenElementsArrayHandler()
        {
            _rnd = new Random();
        }

        protected override string StartInfo => 
            $"Enter 2 positive integers width end height separated by a space or press enter for use default 10 x 10 size...";

        protected override string HandleData(string data)
        {
            int width = DefaultWidth;
            int height = DefaultHeight;

            if (!string.IsNullOrWhiteSpace(data))
            {
                var splitedData = data.Trim().Split().Where(s => !string.IsNullOrEmpty(s)).ToList();

                if (splitedData.Count() != 2
                    || !int.TryParse(splitedData[0], out width)
                    || !int.TryParse(splitedData[1], out height))
                    throw new ArgumentException($"parse \"{ data }\" failed", nameof(data));
            }

            StringBuilder result = new StringBuilder();

            result.Append($"Generated array:{ Environment.NewLine }");
            var array = ArraysUtils.GenerateArray(new[] { width, height }, () => _rnd.Next(ValueDownLimit, ValueUpLimit));
            Add2DArrayToStringBilder(result, array);

            result.Append($"Sum even elements:{ Environment.NewLine }");
            result.Append($"{ CalcEvenElements(array, (int a, int b) => a + b) }{ Environment.NewLine }");

            return result.ToString();
        }

        private void Add2DArrayToStringBilder(StringBuilder stringBuilder, Array array)
        {
            if (array.Rank != 2)
                throw new ArgumentException($"{ nameof(array) } must have rank = 2", nameof(array));

            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    stringBuilder.Append((string.Format($"{{0,{ Math.Max(ValueDownLimit.ToString().Length, ValueUpLimit.ToString().Length) + 1 }}}",
                                           $"{ array.GetValue(i, j) } ")));
                }

                stringBuilder.Append(Environment.NewLine);
            }
        }

        // Индексация считается с 0
        // Элемент будет четным, если сумма номеров его позиций по всем измерениям четна (количество измерений произвольное)
        private T CalcEvenElements<T>(Array array, Func<T, T, T> summator)
        {
            int rank = array.Rank;
            int[] degreesLengths = new int[rank];
            for (int i = 0; i < rank; i++)
                degreesLengths[i] = array.GetLength(i);

            T result = default;
            int[] currentIndexes = new int[rank];
            for (; ; )
            {
                if (currentIndexes.Sum() % 2 == 0)
                    result = summator(result, (T)array.GetValue(currentIndexes));

                for (int k = rank - 1; ; k--)
                {
                    if (k < 0)
                        return result;

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
