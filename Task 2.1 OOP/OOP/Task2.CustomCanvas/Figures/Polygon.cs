using System;
using System.Collections.Generic;
using System.Linq;

namespace CustomCanvas.Figures
{
    // вообще он может быть и не абстрактным и норм работать, но тогда придется чекать самопересечения при создании, а это лень, но мб потом..
    public abstract class Polygon : AbstractFigure
    {
        public Polygon(bool contourOnly = false, params Point[] corners)
        {
            foreach (var point in corners)
            {
                _definingPoints.Add(point);
            }

            IsClosed = true;
            ContourOnly = contourOnly;
        }

        public IReadOnlyList<Point> GetConrers() => _definingPoints;

        // Gauss method
        public override double GetArea()
        {
            double result = 0;

            for (int i = 0, j = _definingPoints.Count - 1; i < _definingPoints.Count; j = i++)
            {
                result += _definingPoints[j].X * _definingPoints[i].Y;
            }

            for (int i = 0, j = _definingPoints.Count - 1; i < _definingPoints.Count; j = i++)
            {
                result -= _definingPoints[i].X * _definingPoints[j].Y;
            }

            return 0.5 * Math.Abs(result);
        }

        public override bool IsFillerPoint(Point point)
        {
            bool result = false;

            for (int i = 0, j = _definingPoints.Count - 1; i < _definingPoints.Count; j = i++)
            {
                Point expi = _definingPoints[i];
                Point expj = _definingPoints[j];

                if ((point.Y <= expi.Y && point.Y > expj.Y || point.Y <= expj.Y && point.Y > expi.Y) && expi.Y != expj.Y
                    && point.X > (expj.X * expi.Y - expi.X * expj.Y - (expj.X - expi.X) * point.Y + .0) / (expi.Y - expj.Y))
                    result = !result;
            }

            return result;
        }

        public override List<Point> GetСontoursPoints()
        {
            IEnumerable<Point> result = new List<Point>();

            for (int i = 0, j = _definingPoints.Count - 1; i < _definingPoints.Count; j = i++)
            {
                result = result.Concat(FiguresUtils.CalculatePointsInLineWithoutEnd(_definingPoints[i], _definingPoints[j]));
            }

            return result.ToList();
        }

        public override double GetContoursLength()
        {
            double result = 0;

            for (int i = 0, j = _definingPoints.Count - 1; i < _definingPoints.Count; j = i++)
            {
                result += FiguresUtils.CalculateLineLength(_definingPoints[i], _definingPoints[j]);
            }

            return result;
        }

        public override Point GetLeftTopOfSurroundingRectangle()
        {
            int x = int.MaxValue;
            int y = int.MinValue;

            foreach (var point in _definingPoints)
            {
                if (point.X < x)
                    x = point.X;

                if (point.Y > y)
                    y = point.Y;
            }

            return new Point(x, y);
        }

        public override Rectangle GetSurroundingRectangle()
        {
            int minX = int.MaxValue;
            int maxX = int.MinValue;
            int minY = int.MaxValue;
            int maxY = int.MinValue;

            foreach (var point in _definingPoints)
            {
                if (point.X < minX)
                    minX = point.X;

                if (point.Y < minY)
                    minY = point.Y;

                if (point.X > maxX)
                    maxX = point.X;

                if (point.Y > maxY)
                    maxY = point.Y;
            }

            return new Rectangle(new Point(minX, maxY), maxX - minX, maxY - minY);
        }
    }
}
