using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        private readonly int[] _degrees = { 2, 5, 5 };

        protected override string StartInfo => "Generating array 2 x 5 x 5 and replace positive values...";

        protected override string ReadData() => null;

        protected override bool MakePause => true;

        protected override string HandleData(string data)
        {
            StringBuilder result = new StringBuilder();

            result.Append($"Inicializing array:{ Environment.NewLine }");
            var array = CubicArraysGenerator.Generate(_degrees[0], _degrees[1], _degrees[2], () => _rnd.Next(ValueDownLimit, ValueUpLimit + 1));
            AddArrayToStringBilder(result, array);

            result.Append($"Replacement values:{ Environment.NewLine }");
            ReplaceElementsInCubicArray(array, val => val > 0, 0);
            AddArrayToStringBilder(result, array);

            return result.ToString();
        }

        private void ReplaceElementsInCubicArray<T>(T[,,] array, Predicate<T> isRepacable, T newValue)
        {
            for (int i = 0; i < array.GetLength(0); i++)
                for (int j = 0; j < array.GetLength(1); j++)
                    for (int k = 0; k < array.GetLength(2); k++)
                        if (isRepacable(array[i, j, k]))
                            array[i, j, k] = newValue;
        }

        private void AddArrayToStringBilder<T>(StringBuilder stringBuilder, T[,,] array)
        {
            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    for (int k = 0; k < array.GetLength(2); k++)
                    {
                        stringBuilder.Append
                            (String.Format($"{{0,{ Math.Max(ValueDownLimit.ToString().Length, ValueUpLimit.ToString().Length) }}}",
                                           $"{ array[i, j, k] } "));
                    }

                    stringBuilder.Append(Environment.NewLine);
                }

                stringBuilder.Append(Environment.NewLine);
            }
        }
    }
}
