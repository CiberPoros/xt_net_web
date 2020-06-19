using System;
using System.Collections.Generic;

namespace CustomCanvas.Figures
{
    public class Line : AbstractFigure
    {
        public Line(Point startPoint, Point endPoint)
        {
            if (startPoint == endPoint)
                throw new ArgumentException($"{nameof(startPoint)} can't be equal {nameof(endPoint)}.");

            IsClosed = false;

            _definingPoints.Add(startPoint);
            _definingPoints.Add(endPoint);
        }

        public Point StartPoint { get => _definingPoints[0]; private set => _definingPoints[0] = value; }
        public Point EndPoint { get => _definingPoints[1]; private set => _definingPoints[1] = value; }

        public override double GetArea() => 0;

        public override double GetContoursLength() => FiguresUtils.CalculateLineLength(StartPoint, EndPoint);

        public override Point GetLeftTopOfSurroundingRectangle() => new Point(Math.Min(StartPoint.X, EndPoint.X), Math.Max(StartPoint.Y, EndPoint.Y));

        public override Rectangle GetSurroundingRectangle()
        {
            var leftTop = GetLeftTopOfSurroundingRectangle();
            int maxX = Math.Max(StartPoint.X, EndPoint.X);
            int minY = Math.Min(StartPoint.Y, EndPoint.Y);

            return new Rectangle(leftTop, maxX - leftTop.X, leftTop.Y - minY);
        }

        public override List<Point> GetСontoursPoints() => FiguresUtils.CalculatePointsInLine(StartPoint, EndPoint);

        public override bool IsFillerPoint(Point point) => false;

        public override LinkedList<Point> GetFillerPoints() => new LinkedList<Point>();
    }
}
