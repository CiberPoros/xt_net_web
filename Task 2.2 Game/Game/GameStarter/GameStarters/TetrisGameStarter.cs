using System;
using GameInterface;
using TetrisGame;

namespace GameStarter.GameStarters
{
    class TetrisGameStarter : AbstractGameStarter
    {
        private static readonly string _baseInfoMessage =
            $"Горячие клавиши:{Environment.NewLine}" +
            $"1. Левая стрелка - смещение фигуры влево,{Environment.NewLine}" +
            $"2. Левая стрелка - смещение фигуры вправо,{Environment.NewLine}" +
            $"3. Стрелка вверх - поворот фигуры,{Environment.NewLine}" +
            $"4. Стрелка вниз  - ускорение фигуры;{Environment.NewLine}" +
            $"Нажмите любую клавишу для старта...";

        public override string BaseInfoMessage => _baseInfoMessage;

        public override int GameWidth => 12;

        public override int GameHeight => 10;

        public override string CreateGameResultMessage(AbstractGame game) => $"Игра окончена. Ваш счет: {game.Score}";

        protected override AbstractGame CreateGame() => new Tetris(GameWidth * 5, GameHeight * 5);
    }
}
