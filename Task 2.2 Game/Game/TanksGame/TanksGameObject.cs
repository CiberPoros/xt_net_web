using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomCanvas;
using CustomCanvas.Figures;
using GameInterface;

namespace TanksGame
{
    public abstract class TanksGameObject : AbstractGameObject
    {
        public const int BulletSize = 3;

        public const int Width = 7;
        public const int Height = 7;

        public const int Offset = 2;

        public readonly Rectangle Contour;

        public TanksGameObject(Point leftTop, AbstractGame gameState) : 
            base(new Point(leftTop.X + Width / 2, leftTop.Y - Height / 2), gameState)
        {
            SightDirection = Direction.Up;

            Contour = new Rectangle(leftTop, 5, 5);

            _figures.Add(new Rectangle(new Point(leftTop.X + Offset, leftTop.Y), Offset, Offset));
            _figures.Add(new Rectangle(new Point(leftTop.X, leftTop.Y - Offset), Offset * 3, Offset));
            _figures.Add(new Rectangle(new Point(leftTop.X, leftTop.Y - Offset * 2), Offset, Offset));
            _figures.Add(new Rectangle(new Point(leftTop.X + Offset * 2, leftTop.Y - Offset * 2), Offset, Offset));
        }

        public Direction SightDirection { get; protected set; }

        public abstract TanksGameObjectType GetGameObjType();

        public bool TryShift(Direction direction)
        {
            Point point = default;
            switch (direction)
            {
                case Direction.Letf:
                    point = new Point(-1, 0);
                    break;
                case Direction.Right:
                    point = new Point(1, 0);
                    break;
                case Direction.Up:
                    point = new Point(0, 1);
                    break;
                case Direction.Down:
                    point = new Point(0, -1);
                    break;
                default:
                    break;
            }

            RotateForRestoreDirection(direction);

            Contour.Shift(point.X, point.Y);

            bool canShift = true;
            foreach (var val in Contour.GetСontoursPoints())
            {
                if (val.X < 0 || val.X + 1 >= _gameState.Width || val.Y - 1 < 0 || val.Y >= _gameState.Height)
                    canShift = false;
            }

            if (canShift)
            {
                Shift(point.X, point.Y);
                return true;
            }

            point = new Point(-point.X, -point.Y);
            Contour.Shift(point.X, point.Y);

            return false;
        }

        public virtual Bullet Shoot()
        {
            Point bulletSpawnPoint;
            switch (SightDirection)
            {
                case Direction.Letf:
                    bulletSpawnPoint = new Point(Contour.LeftTop.X - BulletSize, Contour.LeftTop.Y - Offset);
                    break;
                case Direction.Up:
                    bulletSpawnPoint = new Point(Contour.LeftTop.X + Offset, Contour.LeftTop.Y + BulletSize);
                    break;
                case Direction.Right:
                    bulletSpawnPoint = new Point(Contour.LeftTop.X + Width, Contour.LeftTop.Y - Offset);
                    break;
                case Direction.Down:
                    bulletSpawnPoint = new Point(Contour.LeftTop.X + Offset, Contour.LeftTop.Y - Height);
                    break;
                default:
                    throw new Exception();
            }

            var bullet = new Bullet(bulletSpawnPoint, _gameState, SightDirection);

            if (!bullet.ContourIsOutside())
            {
                _gameState.AddGameObject(bullet);
            }

            return bullet;
        }

        private Direction NextDirection(Direction direction)
        {
            switch (direction)
            {
                case Direction.Letf:
                    return Direction.Up;
                case Direction.Right:
                    return Direction.Down;
                case Direction.Up:
                    return Direction.Right;
                case Direction.Down:
                    return Direction.Letf;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void RotateForRestoreDirection(Direction direction)
        {
            while (direction != SightDirection)
            {
                RotateAroundCenter(Math.PI / 2);
                SightDirection = NextDirection(SightDirection);
            }
        }
    }
}
