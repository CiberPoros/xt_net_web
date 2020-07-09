using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Task1.SuperArray;

namespace SuperArrayExamples
{
    class Program
    {
        static void Main(string[] args)
        {
            List<int[]> arrays = new List<int[]>()
            {
                 null,
                 new int[] { },
                 new int[] { 1, 2, 3, 3, 4, 4, 5, 6 },
                 new int[] { 1, 2, 3, 4, 4, 5, 5, 5, 6, 7 },
                 new int[] { int.MaxValue, 1, 10 },
            };

            Console.WriteLine($"{nameof(ArrayExtensions.GetSum)} methot examples:");
            Console.WriteLine();
            foreach (var array in arrays)
                PrintResultOrException(array, array.GetSum);
            Console.WriteLine("------------------------------");
            Console.WriteLine();

            Console.WriteLine($"{nameof(ArrayExtensions.GetAverage)} methot examples:");
            Console.WriteLine();
            foreach (var array in arrays)
                PrintResultOrException(array, array.GetAverage);
            Console.WriteLine("------------------------------");
            Console.WriteLine();

            Console.WriteLine($"{nameof(ArrayExtensions.GetCommonest)} methot examples:");
            Console.WriteLine();
            foreach (var array in arrays)
                PrintResultOrException(array, array.GetCommonest);
            Console.WriteLine("------------------------------");
            Console.WriteLine();
        }

        private static void PrintResultOrException<T, U>(T[] array, Func<U> calculator)
        {
            Console.WriteLine($"Values: {(array == null ? "array is null" : string.Join(", ", array))}");

            try
            {
                Console.WriteLine($"Result: {calculator()}");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Exception! Type: {e.GetType()}; Message: {e.Message}");
            }
            Console.WriteLine();
        }
    }
}
