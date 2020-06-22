using System;
using CustomCanvas;
using GameInterface;

namespace TetrisGame.FallingObjects
{
    public delegate void DropedEventHandler(object sender, EventArgs e);

    public abstract class AbstractFallingObject : TetrisGameObject, IProcessable
    {
        public const double RotateAngle = Math.PI / 2;

        public AbstractFallingObject(Point center, AbstractGame gameState) : base(center, gameState)
        {

        }

        public event DropedEventHandler Droped = delegate { };

        public int Proirity => 2;

        public void Process()
        {
            if (!TryShift(Direction.Down))
            {
                Droped(this, null);
            }
        }

        public bool TryRotateAroundCenter()
        {
            if (CanRotate(RotateAngle))
            {
                RotateAroundCenter(RotateAngle);
                return true;
            }

            return false;
        }

        public bool TryShift(Direction direction)
        {
            Point shiftParams;

            switch (direction)
            {
                case Direction.Letf:
                    shiftParams = new Point(-1, 0);
                    break;
                case Direction.Right:
                    shiftParams = new Point(1, 0);
                    break;
                case Direction.Up:
                    shiftParams = new Point(0, 1);
                    break;
                case Direction.Down:
                    shiftParams = new Point(0, -1);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(direction), $"Invalid {nameof(direction)} value.");
            }

            if (CanShift(shiftParams.X * CellSize, shiftParams.Y * CellSize))
            {
                Shift(shiftParams.X, shiftParams.Y);
                return true;
            }

            return false;
        }

        private bool CanChangePosition(Func<Point, Point> changePosition)
        {
            foreach (var figure in _figures)
            {
                Point newCenterPosition = changePosition(figure.GetCenterMass());

                if (newCenterPosition.X < 0 || newCenterPosition.X > _gameState.Width || newCenterPosition.Y < 0)
                {
                    return false;
                }

                var falledObjects = _gameState.GetGameObjectsByPriority(FalledObject.GetPriority());

                foreach (var falledObj in falledObjects)
                {
                    foreach (var falledObjFigure in falledObj.Figures)
                    {
                        if (falledObjFigure.IsFillerPoint(newCenterPosition))
                            return false;
                    }
                }
            }

            return true;
        }

        private bool CanRotate(double angle)
        {
            double cos = Math.Cos(angle);
            double sin = Math.Sin(angle);

            return CanChangePosition(p => FiguresUtils.RotatePoint(p - Center, cos, sin) + Center);
        }

        private bool CanShift(int dx, int dy) => CanChangePosition(p => p.Shift(dx, dy));
    }
}
