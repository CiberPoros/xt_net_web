using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CustomCanvas;
using GameInterface;

namespace TanksGame
{
    public class Tanks : AbstractGame
    {
        public const int TanksPriorityLimit = 2;

        public const int GameScoreIncrementByKills = 10;

        private static readonly AutoResetEvent updateStateWaitHandler = new AutoResetEvent(true);

        public Tanks(int width, int height) : base(width, height, TanksPriorityLimit)
        {
            DelayTime = 500;
        }

        public Hero Hero { get; private set; }

        public override int DelayTime { get; }

        public override void OnKeyPressed(object sender, ConsoleKeyPressedEventArgs e)
        {
            bool update = false;

            updateStateWaitHandler.WaitOne();
            switch (e.ConsoleKey)
            {
                case ConsoleKey.LeftArrow:
                    update = Hero.TryShift(Direction.Letf);
                    break;
                case ConsoleKey.RightArrow:
                    update = Hero.TryShift(Direction.Right);
                    break;
                case ConsoleKey.UpArrow:
                    update = Hero.TryShift(Direction.Up);
                    break;
                case ConsoleKey.DownArrow:
                    update = Hero.TryShift(Direction.Down);
                    break;
                case ConsoleKey.Enter:
                    update = true;
                    Hero.Shoot();
                    break;
            }

            updateStateWaitHandler.Set();

            if (update)
                UpdateConsole();
        }

        protected override void Init()
        {
            base.Init();

            Hero = new Hero(new Point(Width / 2, Height / 2), this);
            AddGameObject(new EnemyTank(new Point(0, EnemyTank.Height - 1), this));
            AddGameObject(new EnemyTank(new Point(0, Height - 1), this));
            AddGameObject(new EnemyTank(new Point(Width - EnemyTank.Width, EnemyTank.Height - 1), this));
            AddGameObject(new EnemyTank(new Point(Width - EnemyTank.Width, Height - 1), this));
            AddGameObject(Hero);
            UpdateConsole();
        }

        protected override void Process()
        {
            updateStateWaitHandler.WaitOne();

            base.Process();

            HandleKills();

            CheckWin();

            updateStateWaitHandler.Set();

            UpdateConsole();
        }

        protected override List<ConsoleKey> InitHandledKeys()
        {
            return new List<ConsoleKey>()
            { 
                ConsoleKey.LeftArrow,
                ConsoleKey.RightArrow,
                ConsoleKey.UpArrow,
                ConsoleKey.DownArrow,
                ConsoleKey.Enter,
            };
        }

        private void CheckWin()
        {
            if (GetGameObjectsByPriority(EnemyTank.EnemyTankPriority).Count == 0)
            {
                IsGameEnded = true;
                IsWin = true;
            }
        }

        private void HandleKills()
        {
            foreach (var gameObj in GetGameObjectsByPriority(Bullet.GetPriority()))
            {
                var bullet = gameObj as Bullet;

                if (bullet == null)
                    continue;

                for (int i = 0; i < _gameObjectsByPriority.Count; i++)
                {
                    for (int j = _gameObjectsByPriority[i].Count - 1; j >= 0; j--)
                    {
                        if (i == Bullet.GetPriority())
                            continue;

                        var tank = _gameObjectsByPriority[i][j] as TanksGameObject;

                        if (tank == null)
                            continue;

                        HandleKill(tank, bullet);
                    }
                }     
            }
        }

        private void HandleKill(TanksGameObject tank, Bullet bullet)
        {
            foreach (var bulletFigure in bullet.Figures)
            {
                foreach (var tankFigure in tank.Figures)
                {
                    if (tankFigure.IsInterceptedWith(bulletFigure))
                    {
                        if (bullet.Creater == TanksGameObjectType.Hero
                            && tank.GetGameObjType() == TanksGameObjectType.EnemyTank)
                        {
                            RemoveGameObject(tank);
                            Score += GameScoreIncrementByKills;
                        }

                        if (tank.GetGameObjType() == TanksGameObjectType.Hero)
                        {
                            IsGameEnded = true;
                        }

                        break;
                    }
                }
            }
        }
    }
}
