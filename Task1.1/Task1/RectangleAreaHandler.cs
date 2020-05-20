using System;
using System.Collections.Generic;
using System.Linq;
using Common;

namespace Task1
{
    internal class RectangleAreaHandler : Handler
    {
        protected override string StartInfo => "Enter positive integers width and height separated by a space...";

        protected override string HandleData(string data)
        {
            var splitedData = data.Trim()
                                  .Split()
                                  .Where(s => !string.IsNullOrEmpty(s));

            if (splitedData.Count() != 2)
                throw new ArgumentException($"parse \"{ data }\" failed", nameof(data));

            var parsedData = new List<int>(2);

            foreach (var s in splitedData)
            {
                if (int.TryParse(s, out int res))
                    parsedData.Add(res);
                else
                    throw new ArgumentException($"parse \"{ data }\" failed", nameof(data));
            }

            return GetArea(parsedData[0], parsedData[1]).ToString();
        }

        private int GetArea(int width, int height)
        {
            if (width <= 0)
                throw new ArgumentOutOfRangeException(nameof(width), $"{ nameof(width) } must be positive");
            if (height <= 0)
                throw new ArgumentOutOfRangeException(nameof(height), $"{ nameof(height) } must be positive");

            return width * height;
        }
    }
}
