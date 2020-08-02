using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FileManagement;

namespace FileManagementUI
{
    class Program
    {
        private static readonly string observableDirectoryPath = $@"{Environment.CurrentDirectory}\ObservableDirectory";
        private static readonly string backupDirectoryPath = $@"{Environment.CurrentDirectory}\BackupDirectory";

        private const int MaxCountOfEnumElements = 9;
        private const char ExitSymbol = 'q';
        private const ConsoleKey ExitConsoleKey = ConsoleKey.Q;

        private static readonly Dictionary<ConsoleKey, int> _keysForReadEnumFromConsole = new Dictionary<ConsoleKey, int>()
        {
            {ConsoleKey.D1, 1},
            {ConsoleKey.NumPad1, 1},
            {ConsoleKey.D2, 2},
            {ConsoleKey.NumPad2, 2},
            {ConsoleKey.D3, 3},
            {ConsoleKey.NumPad3, 3},
            {ConsoleKey.D4, 4},
            {ConsoleKey.NumPad4, 4},
            {ConsoleKey.D5, 5},
            {ConsoleKey.NumPad5, 5},
            {ConsoleKey.D6, 6},
            {ConsoleKey.NumPad6, 6},
            {ConsoleKey.D7, 7},
            {ConsoleKey.NumPad7, 7},
            {ConsoleKey.D8, 8},
            {ConsoleKey.NumPad8, 8},
            {ConsoleKey.D9, 9},
            {ConsoleKey.NumPad9, 9},
        };

        static void Main(string[] args)
        {
            IFilesObserver observer = new FilesObserver(observableDirectoryPath, backupDirectoryPath);
            observer.StartObserving();
            var val = ReadEnumValueFromConsole<WorkMode>();
            Console.WriteLine(val);
        }

        /// <summary>
        /// Returns enum type or null for exit
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T? ReadEnumValueFromConsole<T>(bool skipNone = true) where T : struct, Enum
        {
            var enumValues = (T[])Enum.GetValues(typeof(T));
            if (enumValues.Length > MaxCountOfEnumElements)
                throw new ArgumentException($"This method can't be used for enums with more than {MaxCountOfEnumElements}",
                    $"Enum type name: {typeof(T)}");

            int startIndex = 1;
            foreach (var enumValue in enumValues)
            {
                if (skipNone && enumValue.ToString().ToUpper() == "NONE")
                    continue;

                Console.WriteLine($"{startIndex}. - {enumValue};");
                startIndex++;
            }

            Console.WriteLine($"{ExitSymbol}. - Exit to previous menu...");

            while (true)
            {
                var key = Console.ReadKey();
                Console.WriteLine();

                if (key.Key == ExitConsoleKey)
                    return null;

                if (_keysForReadEnumFromConsole.TryGetValue(key.Key, out var enumValueNumber))
                {
                    int currentIndex = 1;
                    foreach (var enumValue in enumValues)
                    {
                        if (skipNone && enumValue.ToString().ToUpper() == "NONE")
                            continue;

                        if (currentIndex == enumValueNumber)
                            return enumValue;

                        currentIndex++;
                    }
                }

                Console.WriteLine("Error input. Try again...");
            }
        }
    }
}
