using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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

        public virtual bool IsInterceptedWith(AbstractFigure other)
        {
            if (_definingPoints.Intersect(other._definingPoints).Count() != 0)
                return true;

            foreach (var point in _definingPoints)
                if (other.IsFillerPoint(point))
                    return true;

            // 2-th cicle nesesarry because _definingPoints don't contains all points of a figure
            foreach (var point in other._definingPoints)
                if (IsFillerPoint(point))
                    return true;

            return false;
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

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            AbstractFigure other = obj as AbstractFigure;
            if (other == null)
                return false;

            if (GetType() != other.GetType())
                return false;

            if (_definingPoints.Count != other._definingPoints.Count)
                return false;

            int length = _definingPoints.Count;
            bool result = false;

            for (int i = 0; i < length; i++)
            {
                if (_definingPoints[i] == other._definingPoints[0]) // For the lack of reference to the position in the _definingPoints list
                {
                    result = true;

                    for (int j = 0, k = i; j < length; j++, k++)
                    {
                        if (k >= length)
                            k = 0;

                        if (_definingPoints[k] != other._definingPoints[j])
                            return false;
                    }
                }
            }

            return result;
        }

        public override int GetHashCode()
        {
            int result = 0;

            foreach (var point in _definingPoints)
                result ^= point.GetHashCode();

            return result;
        }

        public override string ToString()
        {
            StringBuilder result = new StringBuilder();

            foreach (var point in _definingPoints)
            {
                result.Append(point.ToString());
                result.Append(' ');
            }

            return result.ToString();
        }
    }
}
