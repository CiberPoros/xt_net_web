using System;
using System.Linq;
using Common;

namespace Task5
{
    internal class ResuidesSumHandler : Handler
    {
        private const int Limit = 1000;

        protected override string StartInfo => 
            $"Summ x from {{ 1, ..., limit }} where x multiple 3 or x multiple 5 { Environment.NewLine }" +
            $"Enter a non negative integer limit or press enter for use default limit = { 1000 }...";

        protected override string HandleData(string data)
        {
            int limit = Limit;
            if (!string.IsNullOrWhiteSpace(data))     
                if (!int.TryParse(data, out limit))
                    throw new ArgumentException($"parse \"{ data }\" failed", nameof(data));

            return Get2ResuidesTypeSum(limit, 3, 5).ToString();
        }

        // За константу, но для двух модулей
        private int Get2ResuidesTypeSum(int upLimit, int modulo1, int modulo2) => 
            GetResuidesSum(upLimit, modulo1)
            + GetResuidesSum(upLimit, modulo2)
            - GetResuidesSum(upLimit, modulo1 * modulo2);

        private int GetResuidesSum(int upLimit, int modulo) => GetArithmeticProgrSumm(1, (upLimit - 1) / modulo, (upLimit - 1) / modulo) * modulo;

        private int GetArithmeticProgrSumm(int start, int end, int count) => (start + end) * count / 2;

        // За линию, но для произвольного числа модулей
        private int GetResuidesTypeSum(int upLimit, params int[] moduloes) =>
            (from val in Enumerable.Range(1, upLimit - 1)
             where moduloes.Any(modulo => val % modulo == 0)
             select val)
            .Sum();
    }
}
