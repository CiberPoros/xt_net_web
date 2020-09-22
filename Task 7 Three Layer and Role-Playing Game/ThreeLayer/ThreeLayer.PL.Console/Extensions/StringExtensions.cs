using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThreeLayer.PL.Console.Extensions
{
    public static class StringExtensions
    {
        public static bool TryCastToNonNegativeInt32(this string value, out int result) => int.TryParse(value, out result) && result >= 0;
    }
}
