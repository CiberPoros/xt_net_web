using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Task1.WeakestLink
{
    class Program
    {
        const int MAX_INTEM_LENGTH = 3;

        const string ERROR_INPUT_MESSAGE = "Ошибка ввода. Повторите попытку...";

        const string COUNT_PARTICIPANTS_DESCRIPTION = "число участников в круге";
        const string LINK_NUMBER_DESCRIPTION = "номер вычеркиваемого человека в раунде";

        private static readonly string _numeralsProcessDescription =
            $"Считалка началась. Каждый раунд вычеркивается один человек из круга.{Environment.NewLine}" +
            $"Если людей в круге изначально больше 30, то будет выведен только результат.";

        static void Main()
        {
            int participantsCount = ReadPositiveIntFromConsole(COUNT_PARTICIPANTS_DESCRIPTION);
            int linkNumber        = ReadPositiveIntFromConsole(LINK_NUMBER_DESCRIPTION);

            Console.WriteLine();
            Console.WriteLine(_numeralsProcessDescription);
            Console.WriteLine();

            LinkedList<int> participants = new LinkedList<int>();
            for (int i = 1; i < participantsCount + 1; i++)
                participants.AddLast(i);

            InputStateToConsole(participants);
            Console.WriteLine();

            StartNumerals(participants, linkNumber, participantsCount <= 30);
        }

        private static int ReadPositiveIntFromConsole(string description)
        {
            Console.WriteLine($"Введите целое положительное число - {description}:");

            while (true)
            {
                if (int.TryParse(Console.ReadLine(), out int result) && result > 0)
                    return result;

                Console.WriteLine(ERROR_INPUT_MESSAGE);
            }
        }

        private static void StartNumerals<T>(LinkedList<T> participants, int linkNumber, bool showProcess)
        {
            int count = participants.Count;
            var orderedParticipants = participants.ToArray();

            Dictionary<T, bool> removed = participants.ToDictionary(value => value, value => false);

            var node = participants.First;
            for (int i = 0; i < count - linkNumber + 1; i++)
            {
                for (int j = 0; j < linkNumber; j++)    
                    node = node.Next ?? participants.First;         

                removed[(node.Previous ?? participants.Last).Value] = true;
                participants.Remove(node.Previous ?? participants.Last);

                if (showProcess)
                    NumeralsStateToConsole(orderedParticipants, removed, i + 1);
            }

            ResultNumeralsToConsole(orderedParticipants, removed);
        }

        private static void NumeralsStateToConsole<T>(
            T[] orderedParticipants, Dictionary<T, bool> removed, int roundNumber, char separator = ' ')
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append($"Раунд {roundNumber, MAX_INTEM_LENGTH}: ");

            foreach (var val in orderedParticipants)
                stringBuilder.Append(string.Format($"{{0, {MAX_INTEM_LENGTH}}}", removed[val] ? " " : ($"{val}{separator}")));
           
            Console.WriteLine(stringBuilder.ToString().TrimEnd(separator));
        }

        private static void ResultNumeralsToConsole<T>(T[] orderedParticipants, Dictionary<T, bool> removed, char separator = ' ')
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("Дальнейшие вычеркивания невозможны. Оставшиеся участники в круге: ");

            foreach (var val in orderedParticipants)
            {
                if (!removed[val])
                {
                    stringBuilder.Append(val);
                    stringBuilder.Append(separator);
                }
            }

            Console.WriteLine();
            Console.WriteLine(stringBuilder.ToString().TrimEnd(separator));
            Console.WriteLine();
        }

        private static void InputStateToConsole<T>(LinkedList<T> participants, char separator = ' ')
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append($"Начальное состояние круга: {Environment.NewLine}");

            foreach (var val in participants)
            {
                stringBuilder.Append(val);
                stringBuilder.Append(separator);
            }

            Console.WriteLine(stringBuilder.ToString().TrimEnd(separator));
        }
    }
}
