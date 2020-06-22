using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameInterface;

namespace GameStarter.GameStarters
{
    public abstract class AbstractGameStarter
    {
        public abstract string BaseInfoMessage { get; }

        public abstract int GameWidth { get; }
        public abstract int GameHeight { get; }

        public void Start()
        {
            Console.Clear();

            Console.SetWindowSize(5 * GameWidth + 3, 5 * GameHeight + 6);

            Console.WriteLine(BaseInfoMessage);

            Console.ReadKey();

            var game = CreateGame();
            game.Start();

            Console.Clear();
            Console.SetCursorPosition(0, 0);

            Console.WriteLine($"Игра окончена. Ваш счет: {game.Score}");
            Console.WriteLine("Нажмите любою клавишу для выхода в основное меню...");

            Console.ReadKey();
        }

        protected abstract AbstractGame CreateGame();

        public static AbstractGameStarter CreateInstance(GameType gameType)
        {
            switch (gameType)
            {
                case GameType.Tanks:
                    return new TanksGameStarter();
                case GameType.Tetris:
                    return new TetrisGameStarter();
                default:
                    throw new ArgumentOutOfRangeException(nameof(gameType), $"Invalid value of {nameof(gameType)}.");
            }
        }
    }
}
