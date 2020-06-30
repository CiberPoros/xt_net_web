using System;
using System.Collections.Generic;
using System.Linq;
using CString;

namespace Task2.TextAnalysis
{
    class Program
    {
        const int WORD_LENGTH_FOR_FORMAT = 25;
        const int COUNT_WORDS_FORMAT_LENGTH = 10;
        const int FREQUENCIES_FORMAT_LENGTH = 8;

        private static readonly char[] separators = new char[] { ' ', ',', '.', '!', ';', ';', '?', '(', ')', '-', '_', '\'', '"', '@' };

        static void Main()
        {
            Console.WriteLine("Введите текст...");

            var text = CustomString.CreateInstance(Console.ReadLine().ToCharArray());

            var frequencies = CalculateStringsFrequencies(text, false, separators);
            var wordsCount = frequencies.Values.Sum();

            Console.WriteLine($"Всего слов: {wordsCount}");
            Console.WriteLine();
            Console.WriteLine($"{CustomString.CreateInstance("Строка".ToCharArray()).FormatCenter(WORD_LENGTH_FOR_FORMAT)}| Количество | Частота |");

            foreach (var kvp in frequencies)
            {
                Console.WriteLine(
                    $"{kvp.Key, -WORD_LENGTH_FOR_FORMAT}" +
                    $"| {CustomString.CreateInstance(kvp.Value.ToString().ToCharArray()).FormatCenter(COUNT_WORDS_FORMAT_LENGTH)} " +
                    $"| {CustomString.CreateInstance(((kvp.Value + .0) / wordsCount).ToString("0.#####").ToCharArray()).FormatCenter(FREQUENCIES_FORMAT_LENGTH)}" +
                    $"| ");
            }
        }

        private static Dictionary<CustomString, int> CalculateStringsFrequencies(CustomString text, bool dependOnRegister, params char[] separators)
        {
            if (!dependOnRegister)
                text = text.ReplaceByDelegate(c => char.ToLower(c));

            Dictionary<CustomString, int> result = new Dictionary<CustomString, int>();

            List<char> currentStringSymbols = new List<char>();
            for (int i = 0; i < text.Length; i++)
            {
                if (separators.Contains(text[i]))
                {
                    CustomString str = CustomString.CreateInstance(currentStringSymbols.ToArray());

                    currentStringSymbols.Clear();

                    AddStringToFrequencies(result, str);

                    continue;
                }

                currentStringSymbols.Add(text[i]);
            }

            AddStringToFrequencies(result, CustomString.CreateInstance(currentStringSymbols.ToArray()));

            return result;
        }

        private static void AddStringToFrequencies(Dictionary<CustomString, int> frequencies, CustomString str)
        {
            if (str == CustomString.Empty)
                return;

            if (frequencies.ContainsKey(str))
                frequencies[str]++;
            else
                frequencies.Add(str, 1);
        }
    }
}
