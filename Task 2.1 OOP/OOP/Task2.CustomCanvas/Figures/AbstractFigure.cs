using System;
using System.Collections.Generic;

namespace CustomCanvas.Figures
{
    public abstract class AbstractFigure
    {
        private protected List<Point> _definingPoints;

        public AbstractFigure()
        {
            _definingPoints = new List<Point>();
        }

        public event Action OnUpdate = delegate { };

        public bool IsClosed { get; protected set; }
        public bool ContourOnly { get; protected set; }

        public abstract double GetArea();
        public abstract bool IsFillerPoint(Point point);
        public abstract List<Point> GetСontoursPoints();
        public abstract double GetContoursLength();
        public abstract Point GetLeftTopOfSurroundingRectangle();
        public abstract Rectangle GetSurroundingRectangle();

        public virtual Point GetCenterMass()
        {
            int sumX = 0;
            int sumY = 0;

            foreach (var point in _definingPoints)
            {
                sumX += point.X;
                sumY += point.Y;
            }

            int x = (int)Math.Round((sumX + .0) / _definingPoints.Count);
            int y = (int)Math.Round((sumY + .0) / _definingPoints.Count);

            return new Point(x, y);
        }

        public virtual void Shift(int dx, int dy)
        {
            for (int i = 0; i < _definingPoints.Count; i++)
                _definingPoints[i] = _definingPoints[i].Shift(dx, dy);

            OnUpdate();
        }

        public virtual void RotateAroundOf(Point rotationsCenter, double angle)
        {
            double cos = Math.Cos(angle);
            double sin = Math.Sin(angle);

            for (int i = 0; i < _definingPoints.Count; i++)
                _definingPoints[i] = FiguresUtils.RotatePoint(_definingPoints[i] - rotationsCenter, cos, sin) + rotationsCenter;

            OnUpdate();
        }

        public virtual LinkedList<Point> GetFillerPoints()
        {
            LinkedList<Point> result = new LinkedList<Point>();

            if (!IsClosed || ContourOnly)
            {
                return result;
            }

            var rectangle = GetSurroundingRectangle();

            for (int i = 0; i < rectangle.Width; i++)
            {
                for (int j = 0; j < rectangle.Height; j++)
                {
                    Point currentPoint = new Point(rectangle.LeftTop.X + i, rectangle.LeftTop.Y - j);

                    if (IsFillerPoint(currentPoint))
                    {
                        result.AddLast(currentPoint);
                    }
                }
            }

            return result;
        }
    }
}
