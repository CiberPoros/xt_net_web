using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameInterface;
using TanksGame;

namespace GameStarter.GameStarters
{
    public class TanksGameStarter : AbstractGameStarter
    {
        private static readonly string _baseInfoMessage =
            $"Горячие клавиши:{Environment.NewLine}" +
            $"1. Левая стрелка - движение вашего танчика влево,{Environment.NewLine}" +
            $"2. Правая стрелка - движение вашего танчика вправо,{Environment.NewLine}" +
            $"3. Стрелка вверх - движение вашего танчика вверх,{Environment.NewLine}" +
            $"4. Стрелка вниз - движение вашего танчика вниз,{Environment.NewLine}" +
            $"5. Ввод - выстрел;{Environment.NewLine}" +
            $"Нажмите любую клавишу для старта...";

        public override int GameWidth => 30;

        public override int GameHeight => 10;

        public override string BaseInfoMessage => _baseInfoMessage;

        public override string CreateGameResultMessage(AbstractGame game)
        {
            if (game.IsWin)
                return $"Игра окончена. Вы победили. Ваш счет: {game.Score}";
            else 
                return $"Игра окончена. Вы проиграли. Ваш счет: {game.Score}";
        }

        protected override AbstractGame CreateGame() => new Tanks(GameWidth * 5, GameHeight * 5);
    }
}
