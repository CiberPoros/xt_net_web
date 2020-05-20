using System;
using System.Text;
using Common;

namespace Task2
{
    internal class ShiftedLeftPiramidHandler : Handler
    {
        protected override string StartInfo => "Enter a non negative integer...";

        protected override string HandleData(string data)
        {
            if (int.TryParse(data, out int height))
                return GetPiramid(height);
            else
                throw new ArgumentException($"parse \"{ data }\" failed", nameof(data));
        }

        private string GetPiramid(int height, char brickSymbol = '*', char spaceSymbol = ' ')
        {
            if (height < 0)
                throw new ArgumentOutOfRangeException(nameof(height), $"{ nameof(height) } must be non negative");

            StringBuilder piramid = new StringBuilder(height * height + height);
            for (int i = 0; i < height; i++)
            {
                piramid.Append(brickSymbol, i + 1);
                piramid.Append(spaceSymbol, height - i - 1);
                piramid.Append(Environment.NewLine);
            }

            return piramid.ToString();
        }
    }
}
