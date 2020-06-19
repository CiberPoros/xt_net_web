using System;
using System.Text;

namespace Game
{
    class Program
    {
        private static readonly string baseInfoMessage =
            $"Горячие клавиши:{Environment.NewLine}" +
            $"1. Левая стрелка - смещение фигуры влево,{Environment.NewLine}" +
            $"2. Левая стрелка - смещение фигуры вправо,{Environment.NewLine}" +
            $"3. Стрелка вверх - поворот фигуры,{Environment.NewLine}" +
            $"4. Стрелка вниз  - ускорение фигуры;{Environment.NewLine}" +
            $"Нажмите любую клавишу для старта...";

        // TODO: add the following figures view on right side
        static void Main()
        {
            Console.OutputEncoding = Encoding.Unicode;

            int w = 12;
            int h = 9;
            Console.SetWindowSize(TetrisGame.CellSize * w + 3, TetrisGame.CellSize * h + 5);

            Console.WriteLine(baseInfoMessage);

            while (true)
            {
                Console.ReadKey();

                TetrisGame game = new TetrisGame(w, h);
                int gamePoints = game.Start();

                Console.Clear();
                Console.SetCursorPosition(0, 0);

                Console.WriteLine($"Игра окончена. Ваш счет: {gamePoints}");
                Console.WriteLine("Нажмите любою клавишу для старта...");
            }
        }
    }
}
