using System;
using System.Collections.Generic;

namespace CustomCanvas.Figures
{
    public class Circle : AbstractFigure
    {
        public Circle(Point center, int radius, bool contourOnly = false)
        {
            if (radius <= 0)
                throw new ArgumentOutOfRangeException(nameof(radius), $"Parameter {nameof(radius)} must be positive.");

            Radius = radius;

            _definingPoints.Add(center);

            IsClosed = true;
            ContourOnly = contourOnly;
        }

        public Point Center { get => _definingPoints[0]; private set => _definingPoints[0] = value; }

        public int Radius { get; }

        public override double GetArea()
        {
            if (!ContourOnly)
                return 0;

            return Math.PI * Radius * Radius;
        }
        
        public override bool IsFillerPoint(Point point)
        {
            int dx = Math.Abs(point.X - Center.X);
            int dy = Math.Abs(point.Y - Center.Y);

            return dx * dx + dy * dy < Radius * Radius;
        }

        // Bresenham's algorithm
        public override List<Point> GetСontoursPoints()
        {
            List<Point> result = new List<Point>();

            int x = 0;
            int y = Radius;
            int delta = 1 - 2 * Radius;

            while (y >= 0)
            {
                result.Add(new Point(Center.X + x, Center.Y + y));
                result.Add(new Point(Center.X + x, Center.Y - y));
                result.Add(new Point(Center.X - x, Center.Y + y));
                result.Add(new Point(Center.X - x, Center.Y - y));

                int inaccuracy = 2 * (delta + y) - 1;

                if (delta < 0 && inaccuracy <= 0)
                {
                    delta += 2 * ++x + 1;
                    continue;
                }

                if (delta > 0 && inaccuracy > 0)
                {
                    delta -= 2 * --y + 1;
                    continue;
                }

                delta += 2 * (++x - --y);
            }

            return result;
        }

        public override double GetContoursLength() => 2 * Math.PI * Radius;

        public override Point GetLeftTopOfSurroundingRectangle() => new Point(Center.X - Radius, Center.Y + Radius);

        public override Rectangle GetSurroundingRectangle() => new Rectangle(GetLeftTopOfSurroundingRectangle(), Radius * 2, Radius * 2);
    }
}
