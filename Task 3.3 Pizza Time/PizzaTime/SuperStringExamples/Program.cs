using System;
using SuperString;

namespace SuperStringExamples
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("Введите текст...");

            string input = Console.ReadLine();

            var langType = input.GetLanguageType();
            Console.WriteLine($"Использовалось ли несколько языков (или неизвестные символы): {(langType.IsMixed() ? "да" : "нет")}");
            Console.WriteLine($"Языки, встречаемые в тексте: {langType}");
        }
    }
}
