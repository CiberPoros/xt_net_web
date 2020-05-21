using System;
using System.Text;
using Common;

namespace Task8
{
    internal class PositiveNumbersChangeHandler : Handler
    {
        private readonly static Random _rnd;
        const int ValueDownLimit = -100;
        const int ValueUpLimit = 100;

        static PositiveNumbersChangeHandler()
        {
            _rnd = new Random();
        }

        private readonly int[] _degreesLengths = { 2, 5, 5 };

        protected override string StartInfo => "Generating array 2 x 5 x 5 and replace positive values...";

        protected override string ReadData() => null;

        protected override bool MakePause => true;

        protected override string HandleData(string data)
        {
            StringBuilder result = new StringBuilder();

            result.Append($"Inicializing array:{ Environment.NewLine }");
            var array = ArraysUtils.GenerateArray(_degreesLengths, () => _rnd.Next(ValueDownLimit, ValueUpLimit + 1));
            AddCubicArrayToStringBilder(result, array);

            result.Append($"Replacement values:{ Environment.NewLine }");
            ArraysUtils.ReplaceElements(array, val => val > 0, 0);
            AddCubicArrayToStringBilder(result, array);

            return result.ToString();
        }

        private void AddCubicArrayToStringBilder(StringBuilder stringBuilder, Array array)
        {
            if (array.Rank != 3)
                throw new ArgumentException($"{ array } must have rank = 3", nameof(array));

            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    for (int k = 0; k < array.GetLength(2); k++)
                    {
                        stringBuilder.Append
                            (string.Format($"{{0,{ Math.Max(ValueDownLimit.ToString().Length, ValueUpLimit.ToString().Length) + 1 }}}",
                                           $"{ array.GetValue(i, j, k) } "));
                    }

                    stringBuilder.Append(Environment.NewLine);
                }

                stringBuilder.Append(Environment.NewLine);
            }
        }
    }
}
