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
    public class Bullet : AbstractGameObject, IProcessable
    {
        public const int BulletPriority = 1;

        public const int Width = 2;
        public const int Height = 2;

        public readonly Rectangle Contour;

        public Bullet(Point leftTop, AbstractGame gameState, Direction direction) :
            base(new Point(leftTop.X + Width / 2, leftTop.Y - Height / 2), gameState)
        {
            Contour = new Rectangle(leftTop, Width - 1, Height - 1);

            Direction = direction;

            _figures.Add(new Rectangle(leftTop, Width - 1, Height - 1));
        }

        public TanksGameObjectType Creater { get; set; }

        public Direction Direction { get; }

        public int Proirity => BulletPriority;

        public static int GetPriority() => BulletPriority;

        public override void Shift(Direction direction)
        {
            base.Shift(direction);

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

            Contour.Shift(point.X, point.Y);
        }

        public void Process()
        {
            Shift(Direction);

            if (ContourIsOutside())
                _gameState.RemoveGameObject(this);
        }

        public bool ContourIsOutside()
        {
            foreach (var val in Contour.GetСontoursPoints())
            {
                if (val.X < 0 || val.X + 1 >= _gameState.Width || val.Y - 1 < 0 || val.Y >= _gameState.Height)
                    return true;
            }

            return false;
        }
    }
}
