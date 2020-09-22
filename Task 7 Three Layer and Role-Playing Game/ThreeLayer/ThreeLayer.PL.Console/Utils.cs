using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThreeLayer.PL.Console
{
    internal static class Utils
    {
        public static int ReadNonNegativeIntegerFromConsole()
        {
            System.Console.WriteLine("Введите целое число >= 0");

            while (true)
            {
                if (int.TryParse(System.Console.ReadLine(), out int result) && result >= 0)
                    return result;

                System.Console.WriteLine("Неверный формат входных данных. Повторите попытку...");
            }
        }
    }
}
