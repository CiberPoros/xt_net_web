using System;
using System.Text;
using GameInterface;
using TetrisGame;
using TanksGame;
using GameStarter.GameStarters;

namespace GameStarter
{
    class Starter
    {
        private static readonly string baseInfoMessage =
            $"1. Тетрис (полная играбельная версия),{Environment.NewLine}" +
            $"2. Танчики (базовый функционал);{Environment.NewLine}";

        static void Main()
        {
            Console.OutputEncoding = Encoding.Unicode;

            while (true)
            {
                Console.WriteLine(baseInfoMessage);

                var keyInfo = Console.ReadKey();

                switch (keyInfo.Key)
                {
                    case ConsoleKey.D1:
                        AbstractGameStarter.CreateInstance(GameType.Tetris).Start();
                        break;
                    case ConsoleKey.D2:
                        AbstractGameStarter.CreateInstance(GameType.Tanks).Start();
                        break;
                }

                Console.Clear();
            }
        }
    }
}
