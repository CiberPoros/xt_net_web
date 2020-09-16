using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThreeLayer.PL.Console
{
    class Program
    {
        private static readonly Dictionary<UIActionType, string> _discriprionsOfActions = new Dictionary<UIActionType, string>
        {
            { UIActionType.ADD_USER, "Добавить пользователя;" },
            { UIActionType.GET_ALL_USERS, "Показать всех пользователей;" },
            { UIActionType.REMOVE_USER_BY_ID, "Удалить пользователя по ID;" },
            { UIActionType.ADD_AWARD, "Добавить награду;" },
            { UIActionType.GET_ALL_AWARDS, "Показать все возможные награды;" },
            { UIActionType.REMOVE_AWARD_BY_ID, "Удалить награду по ID;" },
            { UIActionType.ADD_AWARD_TO_USER, "Добавить награду для пользователя;" },
            { UIActionType.REMOVE_AWARD_FROM_USER, "Удалить награду у пользователя..." }
        };

        static void Main(string[] args)
        {
            OutActionTypes();
            var at = ReadActionType();
            System.Console.WriteLine(at);
        }

        public static void OutActionTypes()
        {
            foreach (var kvp in _discriprionsOfActions)
            {
                System.Console.WriteLine($"{(int)kvp.Key} - {kvp.Value}");
            }
        }

        private static UIActionType ReadActionType()
        {
            var res = UIActionType.NONE;

            while (res == UIActionType.NONE)
            {
                var input = System.Console.ReadKey().Key;
                var keyValue = input >= ConsoleKey.D1 && input <= ConsoleKey.D8 ? input - ConsoleKey.D0 : 0;
                if (keyValue == 0)
                    keyValue = input >= ConsoleKey.NumPad1 && input <= ConsoleKey.NumPad8 ? input - ConsoleKey.NumPad0 : 0;

                res = (UIActionType)keyValue;

                if (res == UIActionType.NONE)
                    ClearLastSymbolInConsole();
            }

            return res;
        }

        private static void ClearLastSymbolInConsole()
        {
            System.Console.SetCursorPosition(System.Console.CursorLeft - 1, System.Console.CursorTop);
            System.Console.Write(" ");
            System.Console.SetCursorPosition(System.Console.CursorLeft - 1, System.Console.CursorTop);
        }
    }
}
