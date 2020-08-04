using System;
using System.Collections.Generic;
using System.IO;
using FileManagement.FilesObservers;
using FileManagement.FilesRestorers;
using FileManagement.Interfaces;

namespace FileManagementUI
{
    internal class Program
    {
        private const int MaxCountOfEnumElements = 9;
        private const char ExitSymbol = 'q';
        private const ConsoleKey ExitConsoleKey = ConsoleKey.Q;

        private const string LoggingHasStartedMessage = "Logging has started. To stop logging close the program.";
        private const string LoggingProcessMessage = "Logging...";
        private const string EnterDateMessage = "Enter date with format \"mm/dd/yy hh:mm:ss\":";
        private const string ErrorInputMessage = "Error input. Try again...";
        private const string NoneEnumTypeName = "NONE";
        private const string EnteredFutureDateMessage = "Entered a future date. Last backup will be restored...";
        private const string RestoringIsCompletedMessage = "Restoring is completed!";

        static Program()
        {
            ObservableDirectoryPath = $@"{Environment.CurrentDirectory}\ObservableDirectory";
            PressToExitMessage = $"{ExitSymbol}. - Press to exit...";
            IncorrectEnumFieldsCountErrorMessage = $"This method can't be used for enums with more than {MaxCountOfEnumElements}";

            KeysForReadEnumFromConsole = new Dictionary<ConsoleKey, int>()
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
                {ConsoleKey.NumPad9, 9}
            };
        }

        private static readonly string ObservableDirectoryPath;
        private static readonly string PressToExitMessage;
        private static readonly string IncorrectEnumFieldsCountErrorMessage;

        private static readonly Dictionary<ConsoleKey, int> KeysForReadEnumFromConsole;

        internal static void Main()
        {
            CreateWordDirectory();
            StartHandleUserInput();
        }

        private static void StartHandleUserInput()
        {
            while (true)
            {
                switch (ReadEnumValueFromConsole<WorkMode>())
                {
                    case WorkMode.NONE:
                        break;
                    case WorkMode.OBSERVER:
                        HandleObserve();
                        break;
                    case WorkMode.RESTORER:
                        HandleRestore();
                        break;
                    case null:
                        return;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

        private static void CreateWordDirectory()
        {
            if (!Directory.Exists(ObservableDirectoryPath))
                Directory.CreateDirectory(ObservableDirectoryPath);
        }

        private static void HandleObserve()
        {
            IFilesObserver observer = new FilesObserver(ObservableDirectoryPath);

            Console.WriteLine(LoggingHasStartedMessage);
            Console.WriteLine(LoggingProcessMessage);

            observer.StartObserving();
        }

        private static void HandleRestore()
        {
            IFilesRestorer restorer = new FilesRestorer();
            Console.WriteLine(EnterDateMessage);

            DateTime dateTime;

            while (!DateTime.TryParse(Console.ReadLine(), out dateTime))
            {
                Console.WriteLine(ErrorInputMessage);
            }

            if (dateTime > DateTime.Now)
                Console.WriteLine(EnteredFutureDateMessage);

            restorer.Restore(dateTime, ObservableDirectoryPath);

            Console.WriteLine(RestoringIsCompletedMessage);
            Console.WriteLine();
        }

        /// <summary>
        /// Returns enum type or null for exit
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        private static T? ReadEnumValueFromConsole<T>(bool skipNone = true) where T : struct, Enum
        {
            var enumValues = (T[])Enum.GetValues(typeof(T));
            if (enumValues.Length > MaxCountOfEnumElements)
                throw new ArgumentException(IncorrectEnumFieldsCountErrorMessage,
                    $"Enum type name: {typeof(T)}");

            ShowActionOptionsToConsole(skipNone, enumValues);
            return ReadActionOptionFromConsole(skipNone, enumValues);
        }

        private static T? ReadActionOptionFromConsole<T>(bool skipNone, T[] enumValues) where T : struct, Enum
        {
            while (true)
            {
                var key = Console.ReadKey();
                Console.WriteLine();

                if (key.Key == ExitConsoleKey)
                    return null;

                if (KeysForReadEnumFromConsole.TryGetValue(key.Key, out var enumValueNumber))
                {
                    var currentIndex = 1;
                    foreach (var enumValue in enumValues)
                    {
                        if (skipNone && enumValue.ToString().ToUpper() == NoneEnumTypeName)
                            continue;

                        if (currentIndex == enumValueNumber)
                            return enumValue;

                        currentIndex++;
                    }
                }

                Console.WriteLine(ErrorInputMessage);
            }
        }

        private static void ShowActionOptionsToConsole<T>(bool skipNone, T[] enumValues) where T : struct, Enum
        {
            var startIndex = 1;
            foreach (var enumValue in enumValues)
            {
                if (skipNone && enumValue.ToString().ToUpper() == NoneEnumTypeName)
                    continue;

                Console.WriteLine($"{startIndex}. - {enumValue};");
                startIndex++;
            }

            Console.WriteLine(PressToExitMessage);
        }
    }
}
