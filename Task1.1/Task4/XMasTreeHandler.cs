using System;
using System.Text;
using Common;

namespace Task4
{
    class XMasTreeHandler : Handler
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

            StringBuilder piramid = new StringBuilder();
            for (int currentHeight = 0; currentHeight < height + 1; currentHeight++)
            {
                for (int i = 0; i < currentHeight; i++)
                {
                    piramid.Append(spaceSymbol, currentHeight - (i + 1) + (height - currentHeight));
                    piramid.Append(brickSymbol, i * 2 + 1);
                    piramid.Append(spaceSymbol, currentHeight - (i + 1) + (height - currentHeight));
                    piramid.Append(Environment.NewLine);
                }
            }

            return piramid.ToString();
        }
    }
}
