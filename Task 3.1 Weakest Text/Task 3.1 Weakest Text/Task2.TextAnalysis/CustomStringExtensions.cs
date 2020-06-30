using System;
using CString;

namespace Task2.TextAnalysis
{
    public static class CustomStringExtensions
    {
        public static CustomString FormatCenter(this CustomString str, int symbolsCount)
        {
            int offset = Math.Max(symbolsCount - str.Length, 0);

            char[] res = new char[Math.Max(str.Length, symbolsCount)];

            for (int i = 0; i < offset / 2; i++)
                res[i] = ' ';

            for (int i = 0; i < str.Length; i++)
                res[offset / 2 + i] = str[i];

            for (int i = offset / 2 + str.Length; i < res.Length; i++)
                res[i] = ' ';

            return CustomString.CreateInstance(res);
        }
    }
}
