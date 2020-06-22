using System;
using System.Text;
using GameInterface;
using TetrisGame;

namespace GameStarter
{
    class Starter
    {
        private static readonly string baseInfoMessage =
            $"Горячие клавиши:{Environment.NewLine}" +
            $"1. Левая стрелка - смещение фигуры влево,{Environment.NewLine}" +
            $"2. Левая стрелка - смещение фигуры вправо,{Environment.NewLine}" +
            $"3. Стрелка вверх - поворот фигуры,{Environment.NewLine}" +
            $"4. Стрелка вниз  - ускорение фигуры;{Environment.NewLine}" +
            $"Нажмите любую клавишу для старта...";

        static void Main()
        {
            Console.OutputEncoding = Encoding.Unicode;

            int w = 12;
            int h = 10;
            Console.SetWindowSize(5 * w + 3, 5 * h + 5);

            Console.WriteLine(baseInfoMessage);

            while (true)
            {
                Console.ReadKey();

                AbstractGame game = new Tetris(w * 5, h * 5);
                game.Start();

                Console.Clear();
                Console.SetCursorPosition(0, 0);

                Console.WriteLine($"Игра окончена. Ваш счет: {game.Score}");
                Console.WriteLine("Нажмите любою клавишу для старта...");
            }
        }
    }
}
