using System;
using CustomCanvas;
using GameInterface;

namespace TanksGame
{
    public class EnemyTank : TanksGameObject, IProcessable
    {
        public const int EnemyTankPriority = 2;

        private static readonly Random _random = new Random();

        public EnemyTank(Point leftTop, AbstractGame gameState) : base(leftTop, gameState)
        {

        }

        public int Proirity => EnemyTankPriority;

        public override TanksGameObjectType GetGameObjType() => TanksGameObjectType.EnemyTank;

        public void Process()
        {
            if (_gameState.TickNumber % 5 == 0)
            {
                int rnd = _random.Next(4);

                switch (rnd)
                {
                    case 0:
                        TryShift(Direction.Letf);
                        break;
                    case 1:
                        TryShift(Direction.Up);
                        break;
                    case 2:
                        TryShift(Direction.Down);
                        break;
                    case 3:
                        TryShift(Direction.Right);
                        break;
                    default:
                        break;
                }


                if (_random.Next(3) == 1)
                    Shoot();
            }
        }

        public override Bullet Shoot()
        {
            var bullet = base.Shoot();

            bullet.Creater = TanksGameObjectType.EnemyTank;

            return bullet;
        }
    }
}
